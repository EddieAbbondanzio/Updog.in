import { ApiInteractor } from '@/core/api-interactor';
import { Comment } from '@/comment/domain/comment';
import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';
import { CommentFinderByPostParams } from './comment-finder-by-post-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderByPost extends CommentApiInteractor<CommentFinderByPostParams, Comment[]> {
    public async handle(input: CommentFinderByPostParams): Promise<Comment[]> {
        const response = await this.http.get<Comment[]>(`/post/${input.postId}/comment/`);
        const items = response.data.map(ci => this.commentMapper.map(ci));

        return items;
    }
}
