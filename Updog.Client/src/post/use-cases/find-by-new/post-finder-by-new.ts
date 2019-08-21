import { ApiInteractor } from '@/core/api-interactor';
import { PaginationInfo } from '@/core/pagination-info';
import { Post } from '@/post/common/post';
import { PostMapper } from '@/post/common/post-mapper';
import { PostApiInteractor } from '@/post/common/post-api-interactor';

/**
 * API interactor to find new posts.
 */
export class PostFinderByNew extends PostApiInteractor<PaginationInfo, Post[]> {
    public async handle(input: PaginationInfo): Promise<Post[]> {
        const response = await this.http.get<Post[]>(`/post/new/`, { params: input });

        return response.data.map(postInfo => {
            return this.postMapper.map(postInfo);
        });
    }
}
