using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace VisitorLog.Services.Auth
{
    public class SimpleAuthStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromMinutes(30);
        private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        private ClaimsPrincipal _current;
        private Timer? _logoutTimer;

        public SimpleAuthStateProvider()
        {
            _current = _anonymous;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_current));
        }

        public Task SignInAsync(string userName, TimeSpan? timeout = null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName)
            }, authenticationType: "SimpleAuth");

            _current = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));

            StartAutoLogout(timeout ?? _defaultTimeout);
            return Task.CompletedTask;
        }

        public Task SignOutAsync()
        {
            _logoutTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _logoutTimer?.Dispose();
            _logoutTimer = null;

            _current = _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));
            return Task.CompletedTask;
        }

        private void StartAutoLogout(TimeSpan timeout)
        {
            _logoutTimer?.Dispose();

            _logoutTimer = new Timer(_ =>
            {
                _ = SignOutAsync();
            }, null, timeout, Timeout.InfiniteTimeSpan);
        }

        public void Dispose()
        {
            _logoutTimer?.Dispose();
        }
    }
}