using System.Threading.Tasks;
using Data_Access.Models;
using Data_Access.Repositories;
using Application.Interfaces;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private bool _isLoggedIn = false;
        private User _currentUser;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public async Task<bool> Login(string username, string password)
        {
            
            var user = await _userRepository.GetByNameAsync(username);
            if (user == null)
            {
                return false;
            }

            
            if (user.Password == password)
            {
                _isLoggedIn = true;
                _currentUser = user;
                return true;
            }

            return false; 
        }

        public async Task<bool> RegisterAsync(User user)
        {
            var existingUser = await _userRepository.GetByNameAsync(user.Login);
            if (existingUser != null)
            {
                
                return false;
            }

            await _userRepository.AddAsync(user);
            return true;
        }

        public void Logout()
        {
            _isLoggedIn = false;
            _currentUser = null;
        }

        public bool IsLoggedIn()
        {
            return _isLoggedIn;
        }
    }
}
