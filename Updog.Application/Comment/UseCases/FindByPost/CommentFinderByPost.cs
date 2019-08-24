using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByPost : IInteractor<int, IEnumerable<CommentView>> {
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
        public async Task<IEnumerable<CommentView>> Handle(int postId) {
            Post post = await postRepo.FindById(postId);
            IEnumerable<Comment> comments = await commentRepo.FindByPost(post.Id);
            List<CommentView> views = new List<CommentView>();

            foreach (Comment c in comments) {
                views.Add(commentMapper.Map(c));
            }

            return views;
        }
        #endregion
    }
}