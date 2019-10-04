using Updog.Domain;

namespace Updog.Application {
    public interface IPostFactory : IFactory<Post> {
        Post Create(PostCreationData creationData, Space space, User user);
    }
}