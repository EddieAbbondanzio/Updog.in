using System;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new comment.
    /// </summary>
    public sealed class CommentCreator : IInteractor<CommentCreateParams, CommentView> {
        #region Fields
        private ICommentRepo _commentRepo;

        private AbstractValidator<CommentCreateParams> _commentValidator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private ICommentViewMapper _commentMapper;

        /// <summary>
        /// CRUD interface for posts.
        /// </summary>
        private IPostRepo _postRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment creator.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for the DB.</param>
        /// <param name="commentValidator">Validator for verifying comments are good.</param>
        /// <param name="commentMapper">The mapper to build DTOs.</param>
        public CommentCreator(ICommentRepo commentRepo, AbstractValidator<CommentCreateParams> commentValidator, ICommentViewMapper commentMapper, IPostRepo postRepo) {
            _commentRepo = commentRepo;
            _commentValidator = commentValidator;
            _commentMapper = commentMapper;
            _postRepo = postRepo;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentCreateParams input) {
            await _commentValidator.ValidateAndThrowAsync(input);
            Comment? parent = input.ParentId != 0 ? (await _commentRepo.FindById(input.ParentId)) : null;

            Comment comment = new Comment() {
                User = input.User,
                Post = new Post() { Id = input.PostId },
                Parent = parent,
                Body = input.Body,
                CreationDate = DateTime.UtcNow
            };

            await _commentRepo.Add(comment);
            return _commentMapper.Map(comment);
        }
        #endregion
    }
}