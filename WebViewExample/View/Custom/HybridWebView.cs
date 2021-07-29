using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebViewExample.View.Custom
{

    public class HybridWebView : WebView
    {

        Action<string> action;

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public void RegisterAction(Action<string> callback)
        {
            Debug.WriteLine("Debug - Register Action");
            action = callback;
        }

        public void Cleanup()
        {
            Debug.WriteLine("Debug - Clear Action");
            action = null;
        }

        public void InvokeAction(string data)
        {
            Debug.WriteLine("Debug - Invoke Action");
            if (action == null || data == null)
            {
                return;
            }
            Debug.WriteLine("Debug - Data: " + data.ToString());
            action.Invoke(data);
        }

    }

}
