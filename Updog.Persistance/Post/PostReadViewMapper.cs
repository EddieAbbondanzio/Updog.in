using Updog.Domain;

namespace Updog.Persistance {
    public interface IPostReadViewMapper : IMapper<PostRecord, PostReadView> { }

    public sealed class PostReadViewMapper : IPostReadViewMapper {
        #region Publics
        public PostReadView Map(PostRecord source) => new PostReadView() {
            Id = source.Id,
            Type = source.Type,
            Title = source.Title,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasUpdated,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };
        #endregion
    }
}