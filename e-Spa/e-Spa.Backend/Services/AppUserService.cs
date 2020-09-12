using System;
using System.Linq;
using System.Threading.Tasks;
using e_Spa.Backend.Data;
using e_Spa.Backend.Helpers;
using e_Spa.Backend.Interfaces;
using e_Spa.Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace e_Spa.Backend.Services
{
    /// <inheritdoc />
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppUserService(UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        /// <inheritdoc />
        public async Task<(AppUser user, OperationResponse response)> RegisterUserAsync(AppUser appUser)
        {
            appUser.UserName = $"{appUser.FirstName.Substring(0, 3)}{appUser.LastName.Substring(0, 3)}";
            var result = await _userManager.CreateAsync(appUser);
            OperationResponse opResponse;
            if (result.Succeeded)
            {
                opResponse = new OperationResponse
                {
                    Message = "User Created Successfully",
                    Errors = null,
                    IsSuccess = result.Succeeded
                };
                return (appUser, opResponse);
            }

            opResponse = new OperationResponse
            {
                Message = "User Did not create successfully",
                Errors = result.Errors.Select(e=>e.Description),
                IsSuccess = result.Succeeded
            };

            return (null, opResponse);
        }

        /// <inheritdoc />
        public async Task<OperationResponse> AddUserToRoleAsync(AppUser appUser, Role role)
        {
            var user = await _userManager.FindByNameAsync(appUser.UserName);
            if(user is null)
                return new OperationResponse
                {
                    Message = "No user was found with the provided info",
                    Errors = null,
                    IsSuccess = false
                };


            var result = await _userManager.AddToRoleAsync(appUser, Enum.GetName(typeof(Role), role));
            if(result.Succeeded)
                return new OperationResponse
                {
                    Message = "User Successfully added to the role",
                    Errors = result.Errors.Select(e=>e.Description),
                    IsSuccess = result.Succeeded
                };

            return new OperationResponse
            {
                Message = "Unable to add the user to the role",
                Errors = result.Errors.Select(e=>e.Description),
                IsSuccess = result.Succeeded
            };

        }
    }
}
