import { ApiInteractor } from '@/core/api-interactor';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { Post } from '@/post/domain/post';
import { PostMapper } from '@/post/infrastructure/post-mapper';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { PostFinderByUserParams } from './post-finder-by-user-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * API interactor to find posts by the user that created them.
 */
export class PostFinderByUser extends PostApiInteractor<PostFinderByUserParams, PagedResultSet<Post>> {
    public async handle(input: PostFinderByUserParams): Promise<PagedResultSet<Post>> {
        const response = await this.http.get<Post[]>(`/post/user/${input.username}`, { params: input.paginationInfo });

        const pagination = this.getPaginationInfo(response);
        const posts = response.data.map((postInfo: any) => this.postMapper.map(postInfo));

        return new PagedResultSet(posts, pagination);
    }
}
