using Updog.Domain;
using Microsoft.AspNetCore.Mvc;
using Updog.Application.Paging;
using FluentValidation.Results;
using System.Net;
using Updog.Api.Validation;

namespace Updog.Api {
    /// <summary>
    /// Base class for controllers to implement.
    /// </summary>
    public abstract class ApiController : ControllerBase {
        #region Properties
        /// <summary>
        /// The current user of the site interacting with the API.
        /// </summary>
        public new User? User => base.User as User;
        #endregion

        #region Helpers
        /// <summary>
        /// Set the "fancy" Content-Range header atop the HTTP response.
        /// </summary>
        /// <param name="paginationInfo">The pagination info to set.</param>
        protected void SetContentRangeHeader(PaginationInfo paginationInfo) {
            int pageStart = paginationInfo.PageNumber * paginationInfo.PageSize;
            int pageEnd = pageStart + paginationInfo.PageSize - 1;
            Response.Headers.Add("Content-Range", $"{pageStart}-{pageEnd}/{paginationInfo.TotalRecordCount}");
        }

        /// <summary>
        /// Create an internal server error response object.
        /// </summary>
        /// <param name="body">The body to send back.</param>
        protected ObjectResult InternalServerError(object? body = null) => StatusCode((int)HttpStatusCode.InternalServerError, body);

        /// <summary>
        /// Generate a validation failure response message to send back to the client.
        /// </summary>
        /// <param name="validationResult">The validation result to convert.</param>
        protected ObjectResult ValidationFailure(ValidationResult validationResult) {
            return StatusCode((int)HttpStatusCode.BadRequest, ValidationFailureFactory.FromResult(validationResult));
        }
        #endregion
    }
}