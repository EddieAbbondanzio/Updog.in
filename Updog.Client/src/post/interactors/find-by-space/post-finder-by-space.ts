import { Post } from '@/post/domain/post';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { PostFindBySpaceParams } from './post-find-by-space-params';

/**
 * API interactor to find new posts.
 */
export class PostFinderBySpace extends PostApiInteractor<PostFindBySpaceParams, PagedResultSet<Post>> {
    public async handle(input: PostFindBySpaceParams): Promise<PagedResultSet<Post>> {
        const response = await this.http.get<Post[]>(`/space/${input.space}/post/new`, { params: input.pagination });

        const pagination = this.getPaginationInfo(response);
        const posts = response.data.map((postInfo: any) => this.postMapper.map(postInfo));

        return new PagedResultSet(posts, pagination);
    }
}
