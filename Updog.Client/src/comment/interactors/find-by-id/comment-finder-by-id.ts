import { Comment } from '@/comment';
import { CommentApiInteractor } from '@/comment';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderById extends CommentApiInteractor<number, Comment> {
    public async handle(input: number): Promise<Comment> {
        const response = await this.http.get<Comment>(`/comment/${input}`);
        return this.commentMapper.map(response.data);
    }
}
