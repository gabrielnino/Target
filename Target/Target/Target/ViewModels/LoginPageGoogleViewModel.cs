using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target.ViewModels
{
	public class LoginPageGoogleViewModel : ViewModelBase
    {

        public LoginPageGoogleViewModel(INavigationService navigationService) : base(navigationService)
        {
            var requestUrl = "https://accounts.google.com/signin/oauth/oauthchooseaccount?";
            var responseType = "response_type=code";
            var scope = "scope=openid";
            var redirectUris = "redirect_uri=https://www.eltiempo.com/";
            var clientId = "client_id=595828975376-mt7u7gidfhos9gb701m8b7d6fn0t69gq.apps.googleusercontent.com";

            var url = requestUrl + responseType + "&" + scope + "&" + redirectUris + "&" + clientId;


            var webView = new WebView
            {
                Source = url,
                HeightRequest = 1
            };
            webView.Navigated += WebViewOnNavigated;
        }


        private void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
