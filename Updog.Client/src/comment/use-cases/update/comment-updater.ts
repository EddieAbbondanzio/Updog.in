import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';
import { CommentUpdateParams } from './comment-update-params';
import { Comment } from '@/comment/domain/comment';

/**
 * Interactor to update an existing post.
 */
export class CommentUpdater extends CommentApiInteractor<CommentUpdateParams, Comment> {
    public async handle(input: CommentUpdateParams): Promise<Comment> {
        const response = await this.http.patch<Comment>(`/comment/${input.commentId}`, { body: input.body });
        return this.commentMapper.map(response.data);
    }
}
