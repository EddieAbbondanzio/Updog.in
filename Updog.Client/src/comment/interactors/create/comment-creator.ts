import { CommentCreateParams } from './comment-create-params';
import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';
import { Comment } from '@/comment/domain/comment';

/**
 * Interactor to create a new comment.
 */
export class CommentCreator extends CommentApiInteractor<CommentCreateParams, Comment> {
    public async handle(input: CommentCreateParams): Promise<Comment> {
        const response = await this.http.post<Comment>('/comment/', input);
        return this.commentMapper.map(response.data);
    }
}
