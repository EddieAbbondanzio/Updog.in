import { ApiInteractor } from '@/core/api-interactor';
import { PaginationInfo } from '@/core/pagination-info';
import { PostInfo } from '@/post/common/post-info';

/**
 * API interactor to find new posts.
 */
export class PostFinderByNew extends ApiInteractor<PaginationInfo, PostInfo[]> {
    public async handle(input: PaginationInfo): Promise<PostInfo[]> {
        const response = await this.http.get<PostInfo[]>(`/post/new/`, { params: input });

        return response.data.map(postInfo => {
            return new PostInfo(
                postInfo.id,
                postInfo.type,
                postInfo.title,
                postInfo.body,
                postInfo.author,
                postInfo.date
            );
        });
    }
}
