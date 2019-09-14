import { Comment } from '@/comment';
import { CommentApiInteractor } from '@/comment';
import { CommentFinderByPostParams } from './comment-finder-by-post-params';

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
