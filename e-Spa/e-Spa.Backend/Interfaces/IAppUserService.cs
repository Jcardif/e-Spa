using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Spa.Backend.Helpers;
using e_Spa.Backend.Models;

namespace e_Spa.Backend.Interfaces
{
    /// <summary>
    /// Model to handle different user operations on the database
    /// </summary>
    public interface IAppUserService
    {
        /// <summary>
        /// Register a nw user and add the user info to the Database
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<(AppUser user, OperationResponse response)> RegisterUserAsync(AppUser appUser);


        /// <summary>
        /// Add a specific user to a particular role
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<OperationResponse> AddUserToRoleAsync(AppUser appUser, Role role);
    }
}
