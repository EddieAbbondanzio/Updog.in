using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class RoleService : IRoleService {
        #region Fields
        private IEventBus eventBus;
        private IRoleFactory roleFactory;
        private IUserRepo userRepo;
        private IRoleRepo roleRepo;
        private ISpaceRepo spaceRepo;
        #endregion

        #region Constructor(s)
        public RoleService(IEventBus eventBus, IRoleFactory roleFactory, IUserRepo userRepo, IRoleRepo roleRepo, ISpaceRepo spaceRepo) {
            this.eventBus = eventBus;
            this.roleFactory = roleFactory;
            this.userRepo = userRepo;
            this.roleRepo = roleRepo;
            this.spaceRepo = spaceRepo;
        }
        #endregion

        #region Publics
        public async Task<bool> IsUserAdmin(string username) => await roleRepo.IsUserAdmin(username);
        public async Task<bool> IsUserModerator(string username, string space) => await roleRepo.IsUserModerator(username, space);

        public async Task AddAdmin(string username, User user) {
            User newAdmin = await GetUserOrThrow(username);

            Role adminRole = roleFactory.CreateAdminRole(newAdmin);
            await roleRepo.Add(adminRole);

            await eventBus.Dispatch(new AdminAddedEvent(newAdmin));
        }

        public async Task AddModeratorToSpace(string username, string spaceName, User user) {
            User newMod = await GetUserOrThrow(username);
            Space space = await GetSpaceOrThrow(spaceName);

            Role modRole = roleFactory.CreateModeratorRole(newMod, space.Name);
            await roleRepo.Add(modRole);

            await eventBus.Dispatch(new ModeratorAddedToSpaceEvent(space, newMod));
        }

        public async Task RemoveAdmin(string username, User user) {
            User oldAdmin = await GetUserOrThrow(username);

            Role? adminRole = await roleRepo.FindAdminRole(oldAdmin);

            if (adminRole == null) {
                throw new InvalidOperationException($"User {oldAdmin.Username} was not an admin");
            }

            await roleRepo.Delete(adminRole);
            await eventBus.Dispatch(new AdminRemovedEvent(oldAdmin));
        }

        public async Task RemoveModeratorFromSpace(string username, string spaceName, User user) {
            User oldMod = await GetUserOrThrow(username);
            Space space = await GetSpaceOrThrow(spaceName);

            Role? modRole = await roleRepo.FindModeratorRole(user, spaceName);

            if (modRole == null) {
                throw new InvalidOperationException($"User {oldMod.Username} was not a mod of space {space}.");
            }

            await roleRepo.Delete(modRole);
            await eventBus.Dispatch(new ModeratorRemovedFromSpaceEvent(space, user));
        }
        #endregion

        #region Privates
        private async Task<User> GetUserOrThrow(string username) =>
            await userRepo.FindByUsername(username) ?? throw new NotFoundException($"User {username} was not found");

        private async Task<Space> GetSpaceOrThrow(string name) =>
            await spaceRepo.FindByName(name) ?? throw new NotFoundException($"Space {name} was not found.");
        #endregion
    }
}