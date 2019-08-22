import { ApiInteractor } from '@/core/api-interactor';
import { PaginationInfo } from '@/core/pagination-info';
import { Post } from '@/post/common/post';
import { PostMapper } from '@/post/common/post-mapper';
import { PostApiInteractor } from '@/post/common/post-api-interactor';
import { PostFinderByUserParams } from './post-finder-by-user-params';

/**
 * API interactor to find posts by the user that created them.
 */
export class PostFinderByUser extends PostApiInteractor<PostFinderByUserParams, Post[]> {
    public async handle(input: PostFinderByUserParams): Promise<Post[]> {
        const response = await this.http.get<Post[]>(`/post/user/${input.username}`, { params: input.paginationInfo });

        return response.data.map(postInfo => {
            return this.postMapper.map(postInfo);
        });
    }
}
