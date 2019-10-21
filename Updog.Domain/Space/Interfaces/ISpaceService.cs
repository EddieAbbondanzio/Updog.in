using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISpaceService : IService<Space> {
        Task<Space?> FindByName(string name);
        Task<Space?> FindByComment(int commentId);
        Task<Space?> FindByPost(int postId);
        Task<Space> Create(SpaceCreate data, User user);
        Task Update(string space, SpaceUpdate update, User user);
        Task<bool> DoesSpaceExist(string space);
    }
}