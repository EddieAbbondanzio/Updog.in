using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ISpaceReader : IReader<SpaceReadView> {
        #region Publics
        Task<SpaceReadView?> FindById(int id);
        Task<SpaceReadView?> FindByName(string name);
        #endregion
    }
}