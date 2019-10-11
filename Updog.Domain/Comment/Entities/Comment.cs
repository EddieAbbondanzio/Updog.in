using System;
using System.Collections.Generic;

namespace Updog.Domain {
    /// <summary>
    /// A comment associated with a post.
    /// </summary>
    public sealed class Comment : IEntity, IUserEntity, IVotableEntity, IAuditableEntity, IUpdatable<CommentUpdate>, IDeletable {
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
        public int UserId { get; }
        public VoteStats Votes { get; }
        VotableEntityType IVotableEntity.VotableEntityType => VotableEntityType.Comment;
        public int PostId { get; }
        public int ParentId { get; }
        public string Body { get; private set; }
        public DateTime CreationDate { get; }
        public bool WasUpdated { get; private set; }
        public bool WasDeleted { get; private set; }
        #endregion

        #region Constructor(s)
        internal Comment(CommentCreate creationData, User user) {
            PostId = creationData.PostId;
            Body = creationData.Body;
            ParentId = creationData.ParentId;
            CreationDate = DateTime.UtcNow;
            UserId = user.Id;
            Votes = new VoteStats();
        }

        internal Comment(int id, int userId, int postId, int parentId, string body, VoteStats votes, DateTime creationDate, bool wasUpdated, bool wasDeleted) {
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
        public void Update(CommentUpdate update) {
            if (WasDeleted) {
                throw new InvalidOperationException();
            }

            WasUpdated = true;
            Body = update.Body;
        }

        public void Delete() {
            this.WasDeleted = true;
            Body = "[deleted]";
        }

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