import { CommentFinderByUserParams } from './comment-finder-by-user-params';
import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';
import { Comment } from '@/comment/domain/comment';
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
