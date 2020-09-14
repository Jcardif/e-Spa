using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Spa.Backend.Helpers
{
    /// <summary>
    /// Returned after every user operation on the database
    /// </summary>
    public class OperationResponse
    {
        /// <summary>
        /// Message with info about the operation carried out on the user in the database
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Whether the operation was successful or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// A list of errors that might have occurred during the operation
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
