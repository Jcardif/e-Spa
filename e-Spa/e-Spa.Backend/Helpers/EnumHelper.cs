using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Spa.Backend.Helpers
{
    /// <summary>
    ///     Represents an Enum of Roles
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// A salon manager can manage one or more salons
        /// </summary>
        SalonManager,

        /// <summary>
        /// Salon Client cannot manage a salon
        /// </summary>
        SalonClient
    }
}
