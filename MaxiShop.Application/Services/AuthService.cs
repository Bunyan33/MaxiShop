using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _applicationUser = new();
        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            _applicationUser.FirstName = register.FirstName;
            _applicationUser.LastName = register.LastName;
            _applicationUser.Email = register.Email;
            _applicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(_applicationUser, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_applicationUser, "ADMIN");
            }

            return result.Errors;
        }
    }
}
