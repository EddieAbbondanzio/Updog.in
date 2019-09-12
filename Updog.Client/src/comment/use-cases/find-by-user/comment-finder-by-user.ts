import { ApiInteractor } from '@/core/api-interactor';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { Post } from '@/post/domain/post';
import { PostMapper } from '@/post/infrastructure/post-mapper';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { CommentFinderByUserParams } from './comment-finder-by-user-params';
import { Comment } from '@/comment/domain/comment';
import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * API interactor to find coments by the user that created them.
 */
export class CommentFinderByUser extends CommentApiInteractor<CommentFinderByUserParams, PagedResultSet<Comment>> {
    public async handle(input: CommentFinderByUserParams): Promise<PagedResultSet<Comment>> {
        const response = await this.http.get<Comment[]>(`/comment/user/${input.username}`, {
            params: input.paginationInfo
        });

        const pagination = this.getPaginationInfo(response);
        const comments = response.data.map(commentInfo => this.commentMapper.map(commentInfo));

        return new PagedResultSet(comments, pagination);
    }
}
