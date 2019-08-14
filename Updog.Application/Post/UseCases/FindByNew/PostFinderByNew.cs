using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a number of posts based on their creation date.
    /// </summary>
    public sealed class PostFinderByNew : IInteractor<PaginationInfo, PostInfo[]> {
        #region Fields
        private IPostRepo postRepo;

        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by new interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="userRepo">CRUD user repo.</param>
        public PostFinderByNew(IPostRepo postRepo, IUserRepo userRepo) {
            this.postRepo = postRepo;
            this.userRepo = userRepo;
        }
        #endregion


        #region Publics
        public async Task<PostInfo[]> Handle(PaginationInfo input) => await postRepo.FindNewest(input.PageNumber, input.PageSize);
    }
    #endregion
}