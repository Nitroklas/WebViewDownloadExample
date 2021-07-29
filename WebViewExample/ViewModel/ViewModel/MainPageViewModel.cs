using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using WebViewExample.Model;

namespace WebViewExample.ViewModel
{

    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string SourceUrl { get; set; }
        public bool BackButtonIsVisible { get; set; } = false;

        public MainPageViewModel() { }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadURL()
        {
            SourceUrl = Settings.SourceURL;
            Debug.WriteLine("Debug - Load new URL: " + SourceUrl.ToString());

            RefreshURL();
        }

        public void RefreshURL() => OnPropertyChanged(nameof(SourceUrl));

        public void ShowBackButton()
        {
            BackButtonIsVisible = true;
            OnPropertyChanged(nameof(BackButtonIsVisible));
        }

        public void HideBackButton()
        {
            BackButtonIsVisible = false;
            OnPropertyChanged(nameof(BackButtonIsVisible));
        }

    }

}
