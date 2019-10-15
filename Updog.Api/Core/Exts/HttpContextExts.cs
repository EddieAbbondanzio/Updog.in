using System;
using Microsoft.AspNetCore.Http;
using Updog.Domain;

namespace Updog.Api {
    public static class HttpContextExts {
        /// <summary>
        /// Get the active user that made the request.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The actve user.</returns>
        public static User? ActiveUser(this HttpContext context) => context.Items["activeUser"] as User;

        /// <summary>
        /// Set the active user.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="user">The active user.</param>
        public static void SetActiveUser(this HttpContext context, User? user) => context.Items["activeUser"] = user;
    }
}