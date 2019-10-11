using System;
using System.Text.RegularExpressions;

namespace Updog.Domain {
    /// <summary>
    /// A post of the website.
    /// </summary>
    public sealed class Post : IEntity, IUserEntity, IVotableEntity, IAuditableEntity, IUpdatable<PostUpdate>, IDeletable {
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
        public int UserId { get; }
        public VoteStats Votes { get; set; } = new VoteStats();
        VotableEntityType IVotableEntity.VotableEntityType => VotableEntityType.Post;
        public int SpaceId { get; }
        public PostType Type { get; }
        public string Title { get; }
        public string Body { get; private set; } = "";
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool WasUpdated { get; private set; }
        public bool WasDeleted { get; private set; }
        public int CommentCount { get; set; }
        #endregion

        #region Constructor(s)
        internal Post(PostCreate createData, Space space, User user) {
            Type = createData.Type;
            Title = createData.Title;
            Body = createData.Body;
            SpaceId = space.Id;
            UserId = user.Id;

            if (Type == PostType.Link && !Regex.IsMatch(Body, RegexPattern.UrlProtocol)) {
                Body = $"http://{Body}";
            }
        }

        internal Post(int id, int userId, int spaceId, PostType type, string title, string body, DateTime creationDate, int commentCount, VoteStats votes, bool wasUpdated = false, bool wasDeleted = false) {
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

            if (Type == PostType.Link && !Regex.IsMatch(Body, RegexPattern.UrlProtocol)) {
                Body = $"http://{Body}";
            }
        }
        #endregion

        #region Publics
        public void Update(PostUpdate update) {
            if (Type == PostType.Link) {
                throw new InvalidOperationException();
            }

            if (WasDeleted) {
                throw new InvalidOperationException("Post was already deleted");
            }

            WasUpdated = true;
            Body = update.Body;
        }

        public void Delete() {
            Body = "[deleted]";
            WasDeleted = true;
        }

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