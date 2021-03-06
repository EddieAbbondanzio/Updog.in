using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Request to create a new post.
    /// </summary>
    public sealed class PostCreateRequest {
        #region Properties
        public PostType Type { get; set; }

        public string Title { get; set; } = "";

        public string Body { get; set; } = "";

        public string Space { get; set; } = "";
        #endregion
    }
}