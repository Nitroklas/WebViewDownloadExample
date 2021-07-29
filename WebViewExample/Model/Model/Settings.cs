using System;
using Xamarin.Essentials;

namespace WebViewExample.Model
{
    public static class Settings
    {
        #region setting Constants
        private const string KeySourceURL = "sourceURL";
        private static readonly string SourceURLDEFAULT = "https://download.microsoft.com/download/7/8/8/788971A6-C4BB-43CA-91DC-557B8BE72928/Microsoft_Press_eBook_CreatingMobileAppswithXamarinForms_PDF.pdf";
        #endregion

        #region setting Properties
        public static string SourceURL
        {
            get { return Preferences.Get(KeySourceURL, SourceURLDEFAULT); }
            set { Preferences.Set(KeySourceURL, value); }
        }
        #endregion

    }
}
