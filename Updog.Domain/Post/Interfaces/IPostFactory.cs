using Updog.Domain;

namespace Updog.Domain {
    public interface IPostFactory : IFactory<Post> {
        Post Create(PostCreate creationData, Space space, User user);
    }
}