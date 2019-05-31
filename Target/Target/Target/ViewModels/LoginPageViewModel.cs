using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Text.RegularExpressions;
using Target.Services;

namespace Target.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
	{
        IAuthenticationService _authenticationService { get; }
        IPageDialogService _pageDialogService { get; }
        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _pageDialogService = pageDialogService;

            Title = "Login";

            LoginCommand = new DelegateCommand(OnLoginCommandExecuted, LoginCommandCanExecute)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);
            LoginGoogleCommand = new DelegateCommand(OnLoginGoogleCommandExecuted);
        }

        private void OnLoginGoogleCommandExecuted()
        {
            NavigationService.NavigateAsync("/LoginPageGoogle");
        }

        private string _background;

        public string Background
        {
            get { return _background; }
            set { _background = value; }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand LoginCommand { get; }

        public DelegateCommand LoginGoogleCommand { get; }
        private async void OnLoginCommandExecuted()
        {
            IsBusy = true;
            if (_authenticationService.Login(UserName, Password))
            {
                await NavigationService.NavigateAsync("/MainPage");
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Enserio??", "Lee codigo y recuerda que la clave es...", "Lee el codigo...");
            }
            IsBusy = false;
        }

        private bool LoginCommandCanExecute() =>
            !string.IsNullOrWhiteSpace(UserName) 
            && !string.IsNullOrWhiteSpace(Password)
            && Regex.IsMatch(UserName, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
            && IsNotBusy;
    }
}
