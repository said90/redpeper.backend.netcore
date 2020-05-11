using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Enums;
using Redpeper.Helper;
using Redpeper.Model;

namespace Redpeper.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("admin", "admin", "admin", "admin@gmail.com", UserType.Admin);
            await CheckUserAsync("mesero", "mesero", "mesero", "mesero@gmail.com", UserType.Mesero);
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string username, string email, UserType userType)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);

            if (user== null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = username,
                    Email = email,
                    UserType = userType
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Mesero.ToString());
        }


    }
}
