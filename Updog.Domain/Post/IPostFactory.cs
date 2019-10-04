using Updog.Domain;

namespace Updog.Domain {
    public interface IPostFactory : IFactory<Post> {
        Post Create(PostCreationData creationData, Space space, User user);
    }
}