using System;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByPost : IInteractor<int, CommentView[]> {
        #region Fields
        /// <summary>
        /// The underlying repo for finding comments in the database.
        /// </summary>
        private ICommentRepo commentRepo;

        /// <summary>
        /// Mapper to convert a comment into it's DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;

        /// <summary>
        /// CRUD interface for posts in the DB.
        /// </summary>
        private IPostRepo postRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment finder by post.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for comments.</param>
        /// <param name="commentMapper">DTO mapper.</param>
        /// <param name="postRepo">CRUD interface for posts</param>
        public CommentFinderByPost(ICommentRepo commentRepo, IMapper<Comment, CommentView> commentMapper, IPostRepo postRepo) {
            this.commentRepo = commentRepo;
            this.commentMapper = commentMapper;
            this.postRepo = postRepo;
        }
        #endregion

        #region Publics
        public async Task<CommentView[]> Handle(int postId) {
            Post post = await postRepo.FindById(postId);
            Comment[] comments = await commentRepo.FindByPost(post.Id);
            CommentView[] views = new CommentView[comments.Length];

            for (int i = 0; i < comments.Length; i++) {
                views[i] = commentMapper.Map(comments[i]);
            }

            return views;
        }
        #endregion
    }
}