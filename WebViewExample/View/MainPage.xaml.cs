using System;
using System.Diagnostics;
using WebViewExample.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebViewExample
{
    public partial class MainPage : ContentPage
    {
        private bool navigatingIsGettingCanceled = false;
        private bool webViewBackButtonPressed = false;

        readonly MainPageViewModel mainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            BindingContext = mainPageViewModel;

            mainPageViewModel.LoadURL();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            if (hybridWebView.CanGoBack)
            {
                hybridWebView.GoBack();
                return true;
            }

            return false;
        }

        private void HybridWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Debug.WriteLine("Debug - Sender: " + sender.ToString());
            Debug.WriteLine("Debug - Url: " + e.Url.ToString());
            if (!loadingIndicator.IsRunning)
            {
                loadingIndicator.IsRunning = loadingIndicator.IsVisible = true;

                //check every new URL the WebView tries connecting to
                if (hybridWebView == null) { return; }
                if (hybridWebView.Source == null) { return; }

                string nextURL = e.Url;
                string urlProperty = hybridWebView.Source.GetValue(UrlWebViewSource.UrlProperty).ToString();

                if (String.IsNullOrEmpty(nextURL) || nextURL.Contains("about:blank"))
                {
                    return;
                }
                if (Device.RuntimePlatform.Equals(Device.Android))
                {
                    //navigatingIsGettingCanceled = true;
                    try
                    {
                        Browser.OpenAsync(nextURL, new BrowserLaunchOptions
                        {
                            LaunchMode = BrowserLaunchMode.SystemPreferred,
                            TitleMode = BrowserTitleMode.Show,
                            PreferredToolbarColor = Color.White,
                            PreferredControlColor = Color.Red
                        });
                    }
                    catch (Exception ex)
                    {
                        //An unexpected error occured. No browser may be installed on the device.
                        Debug.WriteLine(string.Format("Debug - Following Exception occured: {0}", ex));
                    }
                }
                if (Device.RuntimePlatform.Equals(Device.iOS))
                {
                    mainPageViewModel.ShowBackButton();
                }

                e.Cancel = navigatingIsGettingCanceled;
                if (navigatingIsGettingCanceled)
                {
                    loadingIndicator.IsRunning = loadingIndicator.IsVisible = navigatingIsGettingCanceled = false;
                    Debug.WriteLine("Debug - Navigation getting cancelled");
                    return;
                }

                if (nextURL != urlProperty)
                {
                    hybridWebView.Source.SetValue(UrlWebViewSource.UrlProperty, nextURL);
                }
            }
        }

        private void HybridWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            loadingIndicator.IsRunning = loadingIndicator.IsVisible = false;

            if (webViewBackButtonPressed)
            {
                hybridWebView.GoBack();
                webViewBackButtonPressed = false;
            }
        }

        void ClickedWebViewGoBack(System.Object sender, System.EventArgs e)
        {
            if (hybridWebView.CanGoBack)
            {
                hybridWebView.GoBack();
                mainPageViewModel.HideBackButton();
                webViewBackButtonPressed = true;
            }
        }
    }
}
