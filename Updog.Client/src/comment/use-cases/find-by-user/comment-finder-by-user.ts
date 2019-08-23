import { ApiInteractor } from '@/core/api-interactor';
import { PaginationInfo } from '@/core/pagination-info';
import { Post } from '@/post/common/post';
import { PostMapper } from '@/post/common/post-mapper';
import { PostApiInteractor } from '@/post/common/post-api-interactor';
import { CommentFinderByUserParams } from './comment-finder-by-user-param';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';

/**
 * API interactor to find coments by the user that created them.
 */
export class CommentFinderByUser extends CommentApiInteractor<CommentFinderByUserParams, Comment[]> {
    public async handle(input: CommentFinderByUserParams): Promise<Comment[]> {
        const response = await this.http.get<Comment[]>(`/comment/user/${input.username}`, {
            params: input.paginationInfo
        });

        return response.data.map(commentInfo => {
            return this.commentMapper.map(commentInfo);
        });
    }
}
