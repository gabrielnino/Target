using DryIoc;
using Prism;
using Prism.Ioc;
using System;
using System.Diagnostics;
using Target.Services;
using Target.ViewModels;
using Target.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Target
{
    public partial class App
    {
        public static string UserName { get; set; }
        public static ImageSource ImageUser { get; set; }
        public static string Occupation { get; private set; }

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterForNavigation<LoginPageGoogle, LoginPageGoogleViewModel>();
            containerRegistry.RegisterForNavigation<GoogleProfile, GoogleViewModel>();
        }
        public async static void getGoogleCode(string code)
        {
            try
            {
                if (code != "")
                {
                    GoogleViewModel _vm = new ViewModels.GoogleViewModel();
                    var accessToken = await _vm.GetAccessTokenAsync(code);

                    await _vm.SetGoogleUserProfileAsync(accessToken);
                    App.UserName = _vm.GoogleProfile.DisplayName;
                    string photo = _vm.GoogleProfile.Image.Url;
                    App.Occupation = _vm.GoogleProfile.Occupation;

                    Debug.WriteLine(App.UserName);
                    App.ImageUser = ImageSource.FromUri(new Uri(photo));
                    App.Current.MainPage = new GoogleProfile();

                }
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }
        }
    }
}
