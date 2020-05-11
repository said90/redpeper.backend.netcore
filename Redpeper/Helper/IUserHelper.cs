using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Redpeper.Model;

namespace Redpeper.Helper
{
    public   interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsername(string username);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);


        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);


    }
}
