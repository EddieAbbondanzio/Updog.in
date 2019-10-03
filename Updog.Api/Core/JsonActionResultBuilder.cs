using System;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Application.Validation;

namespace Updog.Api {
    /// <summary>
    /// Builder that generates an ActionResult to return from the controller endpoint.
    /// Implements IOutputPort so it can be passed into application layer.
    /// </summary>
    public sealed class JsonActionResultBuilder : IOutputPort {
        #region Fields
        /// <summary>
        /// The result that was built.
        /// </summary>
        private ActionResult? result;
        #endregion

        #region Publics
        public void Success() => new OkResult();
        public void Success<TResult>(TResult? output = null) where TResult : class => JsonResult(200, output);

        public void NotFound() => JsonResult(404, "Not found");
        public void NotFound<TResult>(TResult? output = null) where TResult : class => JsonResult(404, output);

        public void BadInput() => JsonResult(400, "Bad input");
        public void BadInput<TResult>(TResult? output = null) where TResult : class => JsonResult(400, output);

        public void Unauthorized() => JsonResult(401, "Unauthorized");
        public void Unauthorized<TResult>(TResult? output = null) where TResult : class => JsonResult(401, output);

        public void InvalidOperation() => JsonResult(400, "Invalid operation");
        public void InvalidOperation<TResult>(TResult? output = null) where TResult : class => JsonResult(400, output);

        /// <summary>
        /// Get the ActionResult built by the builder.
        /// </summary>
        /// <returns>The ActionResult that was built.</returns>
        public ActionResult Build() {
            if (result == null) {
                throw new InvalidOperationException("Result was never built");
            }

            var cachedResult = result;

            // Clear out the result so we don't dupe it accidentally.
            result = null;

            return cachedResult;
        }
        #endregion

        #region Privates
        private void JsonResult<TResult>(int statusCode, TResult? output = null) where TResult : class {
            var jResult = new JsonResult(output);
            jResult.StatusCode = statusCode;

            result = jResult;
        }
        #endregion
    }
}