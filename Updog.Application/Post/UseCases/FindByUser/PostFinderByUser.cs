using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a number of posts via the user who created them..
    /// </summary>
    public sealed class PostFinderByUser : IInteractor<PostFinderByUserParam, IEnumerable<PostView>> {
        #region Fields
        private IPostRepo postRepo;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by user interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="postMapper">Mapper to convert post to DTO..</param>
        public PostFinderByUser(IPostRepo postRepo, IMapper<Post, PostView> postMapper) {
            this.postRepo = postRepo;
            this.postMapper = postMapper;
        }
        #endregion


        #region Publics
        public async Task<IEnumerable<PostView>> Handle(PostFinderByUserParam input) {
            IEnumerable<Post> posts = await postRepo.FindByUser(input.Username, input.PaginationInfo);
            List<PostView> views = new List<PostView>();

            foreach (Post p in posts) {
                views.Add(postMapper.Map(p));
            }

            return views;
        }
    }
    #endregion
}