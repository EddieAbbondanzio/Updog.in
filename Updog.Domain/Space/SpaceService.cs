using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class SpaceService : ISpaceService {
        #region Fields
        private IEventBus bus;
        private ISpaceFactory factory;
        private ISpaceRepo repo;
        #endregion

        #region Constructor(s)
        public SpaceService(IEventBus bus, ISpaceFactory factory, ISpaceRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Space> Create(SpaceCreateData data, User user) {
            Space s = factory.Create(data, user);

            await repo.Add(s);
            await bus.Dispatch(new SpaceCreateEvent(s));

            return s;
        }

        public async Task<Space> Update(SpaceUpdateData data, User user) {
            Space? s = await repo.FindByName(data.Space);

            if (s == null) {
                throw new InvalidOperationException();
            };

            s.Description = data.Description;
            await repo.Update(s);
            await bus.Dispatch(new SpaceUpdateEvent(s));

            return s;
        }
        #endregion
    }
}