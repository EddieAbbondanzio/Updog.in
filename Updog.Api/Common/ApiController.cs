using Updog.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Updog.Api {
    /// <summary>
    /// Base class for controllers to implement.
    /// </summary>
    public abstract class ApiController : ControllerBase {
        #region Properties
        /// <summary>
        /// The current user of the site interacting with the API.
        /// </summary>
        public new User User => base.User as User;
        #endregion

        #region Helpers
        /// <summary>
        /// Create an internal server error response object.
        /// </summary>
        /// <param name="body">The body to send back.</param>
        protected ObjectResult InternalServerError(object body = null) => StatusCode(500, body = null);
        #endregion
    }
}