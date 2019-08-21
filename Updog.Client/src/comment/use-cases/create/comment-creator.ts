import { ApiInteractor } from '@/core/api-interactor';
import { CommentCreateParams } from './comment-create-params';
import { Context } from '@/core/context';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';

/**
 * Interactor to create a new comment.
 */
export class CommentCreator extends CommentApiInteractor<CommentCreateParams, Comment> {
    public async handle(input: CommentCreateParams): Promise<Comment> {
        // Crash hard if not authed. The backend will catch this with a 401 response.
        if (Context.login == null) {
            throw new Error('Not logged in!');
        }

        const response = await this.http.post<Comment>('/comment/', input, {
            headers: { Authorization: `Bearer ${Context.login.authToken}` }
        });

        return this.commentMapper.map(response.data);
    }
}
