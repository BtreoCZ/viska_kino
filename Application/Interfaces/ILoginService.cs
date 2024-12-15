using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Models;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        Task<bool> Login(string username, string password);
        Task<bool> RegisterAsync(User user);

        User GetCurrentUser();
        void Logout();
        bool IsLoggedIn();

    }
}
