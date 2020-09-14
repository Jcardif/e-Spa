using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Spa.Backend.Interfaces
{
    /// <summary>
    /// Model to handle different Role operations on the database
    /// </summary>
    public interface IAppRoleService
    {
        /// <summary>
        /// Create the default roles and add them to the Database
        /// </summary>
        /// <returns></returns>
        Task CreateRoleAsync();
    }
}
