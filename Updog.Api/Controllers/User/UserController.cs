using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;

namespace Updog.Api {
    /// <summary>
    /// End point for managing users of the site.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public sealed class UserController : ApiController {
        #region Fields
        private IEventBus bus;
        private QueryHandler<FindUserByUsernameQuery, UserReadView?> userFinder;
        private QueryHandler<IsUsernameAvailableQuery, bool> usernameChecker;
        private CommandHandler<RegisterUserCommand> userRegistrar;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController(
            IEventBus bus,
                QueryHandler<FindUserByUsernameQuery, UserReadView?> userFinder,
                QueryHandler<IsUsernameAvailableQuery, bool> usernameChecker,
                CommandHandler<RegisterUserCommand> userRegistrar
            ) {
            this.bus = bus;
            this.userFinder = userFinder;
            this.usernameChecker = usernameChecker;
            this.userRegistrar = userRegistrar;
        }
        #endregion

        #region Publics
        [HttpHead("{username}")]
        public async Task<IActionResult> IsUsernameAvailable(string username) {
            var isFree = await usernameChecker.Execute(new IsUsernameAvailableQuery(username, User));

            return isFree ? NotFound() : Ok() as IActionResult;
        }

        /// <summary>
        /// Retrieve a user from the backend via their username.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        [HttpGet("{username}")]
        public async Task<IActionResult> FindByUsername(string username) {
            var user = await userFinder.Execute(new FindUserByUsernameQuery(username, User));

            return user != null ? Ok(user) : NotFound() as IActionResult;
        }

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="registration">The new user registration</param>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest req) {
            UserLogin? login = null;

            bus.Listen<UserRegisterEvent>((IDomainEvent e) => {
                UserRegisterEvent registerEvent = (UserRegisterEvent)e;
                login = registerEvent.Login;
            });

            var result = await userRegistrar.Execute(new RegisterUserCommand() {
                Registration = new UserRegistration(req.Username, req.Password, req.Email)
            });

            return login != null ? Ok(login) : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}