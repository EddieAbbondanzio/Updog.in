using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a number of posts based on their creation date.
    /// </summary>
    public sealed class PostFinderByNew : IInteractor<PaginationInfo, PostView[]> {
        #region Fields
        private IPostRepo postRepo;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by new interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="postMapper">Mapper to convert post to DTO..</param>
        public PostFinderByNew(IPostRepo postRepo, IMapper<Post, PostView> postMapper) {
            this.postRepo = postRepo;
            this.postMapper = postMapper;
        }
        #endregion


        #region Publics
        public async Task<PostView[]> Handle(PaginationInfo input) {
            Post[] posts = await postRepo.FindNewest(input);
            return posts.Select((p) => postMapper.Map(p)).ToArray();
        }
    }
    #endregion
}