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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUser _applicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _applicationUser = new();
            _signInManager = signInManager;
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

        public async Task<object> Login(Login login)
        {
            _applicationUser = await _userManager.FindByEmailAsync(login.Email);

            if(_applicationUser == null)
            {
                return "Invalid Email Address";
            }

            var result = await _signInManager.PasswordSignInAsync(_applicationUser, login.Password, isPersistent: true, lockoutOnFailure:true);

            var isValidCredential = await _userManager.CheckPasswordAsync(_applicationUser, login.Password);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return "Your Account is Locked, Contact System Admin";
                }

                if (result.IsNotAllowed)
                {
                    return "Please verify Email Address";
                }

                if(isValidCredential == false)
                {
                    return "Invalid Password";
                }
                else
                {
                    return "Login Failed";
                }
            }
        }
    }
}
