using System;

namespace Updog.Domain {
    /// <summary>
    /// A post of the website.
    /// </summary>
    public sealed class Post : IEntity, IUserEntity, IVotableEntity, IAuditableEntity {
        #region Constants
        /// <summary>
        /// The max # of characters allowed in a post title.
        /// </summary>
        public const int TitleMaxLength = 300;

        /// <summary>
        /// The max # of characters allowed in a post body.
        /// </summary>
        public const int BodyMaxLength = 10_000;

        /// <summary>
        /// The default page size of a page of posts.
        /// </summary>
        public const int PageSize = 25;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public VoteStats Votes { get; set; } = new VoteStats();
        VotableEntityType IVotableEntity.VotableEntityType => VotableEntityType.Post;
        public int SpaceId { get; set; }
        public PostType Type { get; set; }
        public string Title { get; set; } = "";
        public string Body {
            get => body;
            set {
                if (Type == PostType.Link) {
                    throw new InvalidOperationException();
                }

                if (WasDeleted) {
                    throw new InvalidOperationException("Post was already deleted");
                }

                WasUpdated = true;
                body = value;
            }
        }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool WasUpdated { get; private set; }
        public bool WasDeleted { get; private set; }
        public int CommentCount { get; set; }
        #endregion

        #region Fields
        private string body = "";
        #endregion

        #region Constructor(s)
        public Post(PostCreateData creationData, User user) {
            Type = creationData.Type;
            Title = creationData.Title;
            Body = creationData.Body;
            SpaceId = creationData.SpaceId;
            UserId = user.Id;
        }

        public Post(int id, int userId, int spaceId, PostType type, string title, string body, DateTime creationDate, int commentCount, VoteStats votes, bool wasUpdated = false, bool wasDeleted = false) {
            Id = id;
            UserId = userId;
            SpaceId = spaceId;
            Type = type;
            Title = title;
            Body = body;
            CreationDate = creationDate;
            CommentCount = commentCount;
            Votes = votes;
            WasUpdated = wasUpdated;
            WasDeleted = wasDeleted;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Check t osee if the post matches another object.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the post matches</returns>
        public override bool Equals(object obj) {
            Post? p = obj as Post;

            if (p == null) {
                return false;
            }

            return Equals(p);
        }

        /// <summary>
        /// Check to see if the post matches another post.
        /// </summary>
        /// <param name="post">The other post to check.</param>
        /// <returns>True if the post match.</returns>
        public bool Equals(Post post) => Id == post.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}