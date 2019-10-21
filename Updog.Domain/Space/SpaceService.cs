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

        public async Task<Space?> FindByComment(int commentId) => await repo.FindByComment(commentId);

        public async Task<Space?> FindByPost(int postId) => await repo.FindByPost(postId);

        public async Task<Space?> FindByName(string name) => await repo.FindByName(name);

        public async Task<Space> Create(SpaceCreate data, User user) {
            // Check if name is available.
            Space? existing = await repo.FindByName(data.Name);
            if (existing != null) {
                throw new SpaceNameAlreadyInUseException($"{data.Name} is unavailable.");
            }

            Space s = factory.Create(data, user);

            await repo.Add(s);
            await bus.Dispatch(new SpaceCreateEvent(s));

            return s;
        }

        public async Task Update(string space, SpaceUpdate update, User user) {
            Space? s = await repo.FindByName(space);

            if (s == null) {
                throw new NotFoundException($"No space with name {space} found.");
            };

            s.Update(update);
            await repo.Update(s);

            await bus.Dispatch(new SpaceUpdateEvent(s));
        }



        public async Task<bool> DoesSpaceExist(string space) => await repo.Exists(space);
        #endregion
    }
}