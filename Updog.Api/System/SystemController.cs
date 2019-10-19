using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Domain;

namespace Updog.Api {
    [Route("api/system")]
    [ApiController]
    public sealed class SystemController : ApiController {
        #region Fields
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public SystemController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        #region Publics
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmins() {
            var admins = await mediator.Query<FindAdminsQuery, IEnumerable<UserReadView>>(new FindAdminsQuery(User));
            return Ok(admins);
        }

        [HttpPost("admin")]
        [Authorize]
        public async Task<IActionResult> AddAdmin(AdminCreateRequest body) {
            var result = await mediator.Command(new AddAdminCommand(body.Username, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }

        [HttpDelete("admin/{username}")]
        [Authorize]
        public async Task<IActionResult> RemoveAdmin(string username) {
            var result = await mediator.Command(new RemoveAdminCommand(username, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}