using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        INavigationService _navigationService { get; }

        public AuthenticationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool Login(string username, string password)
        {
            if (password.Equals("4dm1n123$", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _navigationService.NavigateAsync("/LoginPage");
        }
    }
}
