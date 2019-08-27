import { ApiInteractor } from '@/core/api-interactor';
import { CommentCreateParams } from './comment-create-params';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';

/**
 * Interactor to create a new comment.
 */
export class CommentCreator extends CommentApiInteractor<CommentCreateParams, Comment> {
    public async handle(input: CommentCreateParams): Promise<Comment> {
        const response = await this.http.post<Comment>('/comment/', input);
        return this.commentMapper.map(response.data);
    }
}
