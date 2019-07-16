using Blurtle.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blurtle.Api {
    /// <summary>
    /// Base class for controllers to implement.
    /// </summary>
    public abstract class ApiController : ControllerBase {
        #region Publics
        /// <summary>
        /// The current user of the site interacting with the API.
        /// </summary>
        public new User User => base.User as User;
        #endregion
    }
}