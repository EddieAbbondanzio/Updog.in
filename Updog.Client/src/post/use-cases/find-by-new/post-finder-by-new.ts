import { ApiInteractor } from '@/core/api-interactor';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { Post } from '@/post/domain/post';
import { PostMapper } from '@/post/infrastructure/post-mapper';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * API interactor to find new posts.
 */
export class PostFinderByNew extends PostApiInteractor<PaginationParams, PagedResultSet<Post>> {
    public async handle(input: PaginationParams): Promise<PagedResultSet<Post>> {
        const response = await this.http.get<Post[]>(`/post/new/`, { params: input });

        const pagination = this.getPaginationInfo(response);
        const posts = response.data.map((postInfo: any) => this.postMapper.map(postInfo));

        return new PagedResultSet(posts, pagination);
    }
}
