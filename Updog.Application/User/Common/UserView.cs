
using System;

namespace Updog.Application {
    /// <summary>
    /// A user DTO.
    /// </summary>
    public sealed class UserView : IView {
        #region Properties
        /// <summary>
        /// Unique ID of the user.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The join date of the user.
        /// </summary>
        public DateTime JoinedDate { get; }

        /// <summary>
        /// The number of upvotes on posts they have.
        /// </summary>
        public int PostKarma { get; }

        /// <summary>
        /// The number of upvotes on comments they have.
        /// </summary>
        public int CommentKarma { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user view.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="username">The display name of the user.</param>
        /// <param name="joinedDate">The date they joined the site.</param>
        /// <param name="postKarma">The number of post upvotes.</param>
        /// <param name="commentKarma">The number of comment upvotes</param>
        public UserView(int id, string username, DateTime joinedDate, int postKarma, int commentKarma) {
            Id = id;
            Username = username;
            JoinedDate = joinedDate;
            PostKarma = postKarma;
            CommentKarma = commentKarma;
        }
        #endregion
    }
}