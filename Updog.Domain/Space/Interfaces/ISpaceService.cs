using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISpaceService : IService<Space> {
        Task<Space> Create(SpaceCreateData data, User user);
        Task<Space> Update(SpaceUpdateData data, User user);
    }
}