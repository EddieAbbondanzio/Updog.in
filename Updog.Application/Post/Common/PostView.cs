using System;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// A post DTO.
    /// </summary>
    public sealed class PostView : IView {
        #region Properties
        /// <summary>
        /// The unique ID of the post.
        /// </summary>
        /// <value></value>
        public int Id { get; }

        /// <summary>
        /// The type flag.
        /// </summary>
        public PostType Type { get; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The text of the post.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The user that posted it.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The space it was submitted to.
        /// </summary>
        public SpaceView Space { get; }

        /// <summary>
        /// The date it was created.
        /// </summary>
        public DateTime CreationDate { get; }

        /// <summary>
        /// How many comments are on the post.
        /// </summary>
        /// <value></value>
        public int CommentCount { get; }

        /// <summary>
        /// If the post has ben editted.
        /// </summary>
        public bool WasUpdated { get; }

        /// <summary>
        /// If the post has been deleted.
        /// </summary>
        public bool WasDeleted { get; }

        /// <summary>
        /// How many upvotes the post has recieved.
        /// </summary>
        public int Upvotes { get; }

        /// <summary>
        /// How many downvotes the post has recieved.
        /// </summary>
        public int Downvotes { get; }

        public VoteView? Vote { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post DTO.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <param name="type">The type flag of the post.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The body of the post.</param>
        /// <param name="user">The OP.</param>
        /// <param name="space">The space it was submitted to..</param>
        /// <param name="creationDate">The date the post was created on.</param>
        /// <param name="commentCount">The comment countof the post</param>
        /// <param name="wasEditted">If the post has been editted</param>
        /// <param name="wasDeleted">If the post has been deleted.</param>
        /// <param name="vote">The vote of the user viewing it</param>
        public PostView(int id, PostType type, string title, string body, UserView user, SpaceView space, DateTime creationDate, int commentCount, bool wasEditted, bool wasDeleted, int upvotes, int downvotes, VoteView? vote = null) {
            Id = id;
            Type = type;
            Title = title;
            Body = body;
            User = user;
            Space = space;
            CreationDate = creationDate;
            CommentCount = commentCount;
            WasUpdated = wasEditted;
            WasDeleted = wasDeleted;
            Upvotes = upvotes;
            Downvotes = downvotes;
            Vote = vote;
        }
        #endregion
    }
}