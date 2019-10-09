using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISpaceService : IService<Space> {
        Task<Space> Create(SpaceCreate data, User user);
        Task<Space> Update(string space, SpaceUpdate update, User user);
    }
}