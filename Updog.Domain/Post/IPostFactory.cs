using Updog.Domain;

namespace Updog.Domain {
    public interface IPostFactory : IFactory<Post> {
        Post Create(PostCreateData creationData, User user);
    }
}