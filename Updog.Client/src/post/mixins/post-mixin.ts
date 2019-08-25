import Component from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/create/post-creator';
import { PostFinderById } from '../use-cases/find-by-id/post-finder-by-id';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { PostFinderByNew } from '../use-cases/find-by-new/post-finder-by-new';
import { PostFinderByUser } from '../use-cases/find-by-user/post-finder-by-user';
import { PostFinderByUserParams } from '../use-cases/find-by-user/post-finder-by-user-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { PostUpdateParams } from '../use-cases/update/post-update-params';
import { PostUpdater } from '../use-cases/update/post-updater';

@Component
export class PostMixin extends Vue {
    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        return new PostCreator().handle(request);
    }

    /**
     * Update a post with the backend.
     * @param request The post update params.
     */
    public async $updatePost(request: PostUpdateParams): Promise<Post> {
        return new PostUpdater().handle(request);
    }

    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findPostById(request: number): Promise<Post> {
        return new PostFinderById().handle(request);
    }

    /**
     * Find a collection of posts by new.
     * @param paginationInfo The pagination info.
     */
    public async $findPostsByNew(paginationInfo: PaginationParams): Promise<PagedResultSet<Post>> {
        return new PostFinderByNew().handle(paginationInfo);
    }

    /**
     * Find a collection of posts for a user.
     * @param username The username to look for.
     * @param paginationInfo The paging info.
     */
    public async $findPostsByUser(username: string, paginationInfo: PaginationParams): Promise<PagedResultSet<Post>> {
        return new PostFinderByUser().handle(new PostFinderByUserParams(username, paginationInfo));
    }
}
