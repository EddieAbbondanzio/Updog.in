using System;
using System.Collections.Generic;

namespace Updog.Domain {
    /// <summary>
    /// A comment associated with a post.
    /// </summary>
    public sealed class Comment : IEntity, IUserEntity, IVotableEntity, IAuditableEntity {
        #region Constants
        /// <summary>
        /// The maximum number of characters in a comment body.
        /// </summary>
        public const int BodyMaxLength = 10_000;

        /// <summary>
        /// Number of comments to get per page.
        /// </summary>
        public const int PageSize = 500;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public VoteStats Votes { get; set; } = new VoteStats();
        VotableEntityType IVotableEntity.VotableEntityType => VotableEntityType.Comment;
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public string Body {
            get => body;
            set {
                if (WasDeleted) {
                    throw new InvalidOperationException();
                }

                WasUpdated = true;
                body = value;
            }
        }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool WasUpdated { get; private set; }
        public bool WasDeleted { get; private set; }
        #endregion

        #region Fields
        private string body = "";
        #endregion

        #region Constructor(s)
        public Comment(CommentCreateData creationData, User user) {
            PostId = creationData.PostId;
            Body = creationData.Body;
            CreationDate = DateTime.UtcNow;
            UserId = user.Id;
        }

        public Comment(int id, int userId, int postId, int parentId, string body, VoteStats votes, DateTime creationDate, bool wasUpdated, bool wasDeleted) {
            Id = id;
            UserId = userId;
            PostId = postId;
            ParentId = parentId;
            Body = body;
            Votes = votes;
            CreationDate = creationDate;
            WasUpdated = wasUpdated;
            WasDeleted = wasDeleted;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if the comment is equal to another object.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the other object matches the comment.</returns>
        public override bool Equals(object obj) {
            Comment? c = obj as Comment;

            if (c == null) {
                return false;
            }

            return Equals(c);
        }

        /// <summary>
        /// Check to see if two comments are equivalent.
        /// </summary>
        /// <param name="c">The other comment to check.</param>
        /// <returns>True if the comments match.</returns>
        public bool Equals(Comment c) => c.Id == this.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion

    }
}