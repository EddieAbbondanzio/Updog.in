using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByPost : IInteractor<CommentFinderByPostParams, IEnumerable<CommentView>> {
        #region Fields
        /// <summary>
        /// The underlying repo for finding comments in the database.
        /// </summary>
        private ICommentRepo _commentRepo;

        /// <summary>
        /// Mapper to convert a comment into it's DTO.
        /// </summary>
        private ICommentViewMapper _commentMapper;

        /// <summary>
        /// CRUD interface for posts in the DB.
        /// </summary>
        private IPostRepo _postRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment finder by post.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for comments.</param>
        /// <param name="commentMapper">DTO mapper.</param>
        /// <param name="postRepo">CRUD interface for posts</param>
        public CommentFinderByPost(ICommentRepo commentRepo, ICommentViewMapper commentMapper, IPostRepo postRepo) {
            _commentRepo = commentRepo;
            _commentMapper = commentMapper;
            _postRepo = postRepo;
        }
        #endregion

        #region Publics
        public async Task<IEnumerable<CommentView>> Handle(CommentFinderByPostParams p) {
            Post post = await _postRepo.FindById(p.PostId);
            IEnumerable<Comment> comments = await _commentRepo.FindByPost(p.PostId);
            List<CommentView> views = new List<CommentView>();

            foreach (Comment c in comments) {
                views.Add(_commentMapper.Map(c));
            }

            return views;
        }
        #endregion
    }
}