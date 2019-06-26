using Microsoft.AspNetCore.Mvc;
using Blurtle.Application;
using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Api {
    /// <summary>
    /// End point for managing users of the site.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UserController : ControllerBase {
        #region Fields
        private IUserService userService;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        /// <param name="userService">The user service to retrieve users with.</param>
        public UserController(IUserService userService) {
            this.userService = userService;
        }
        #endregion

        #region Publics
        public async Task FindUserByUsername() {

            User user = await userService.FindUserByUsername("fuck");
        }

        public async Task RegisterUser() {

        }

        public async Task LoginUser() {

        }

        public async Task IsUsernameAvailable() {

        }
        #endregion
    }
}