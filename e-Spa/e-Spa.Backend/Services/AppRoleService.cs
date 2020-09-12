using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Spa.Backend.Helpers;
using e_Spa.Backend.Interfaces;
using e_Spa.Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace e_Spa.Backend.Services
{
    /// <inheritdoc />
    public class AppRoleService : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleManager"></param>
        public AppRoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <inheritdoc />
        public async Task CreateRoleAsync()
        {
            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await _roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }
    }
}
