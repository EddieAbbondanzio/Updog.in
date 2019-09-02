import { ApiInteractor } from '@/core/api-interactor';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { Post } from '@/post/common/post';
import { PostMapper } from '@/post/common/post-mapper';
import { PostApiInteractor } from '@/post/common/post-api-interactor';
import { CommentFinderByUserParams } from './comment-finder-by-user-params';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';
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
