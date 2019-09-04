import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { User } from '@/user/common/user';
import { PostFinderById } from '../use-cases/find-by-id/post-finder-by-id';
import { PostFinderByNew } from '../use-cases/find-by-new/post-finder-by-new';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { PostFinderByUser } from '../use-cases/find-by-user/post-finder-by-user';
import { PostFinderByUserParams } from '../use-cases/find-by-user/post-finder-by-user-params';
import { PostCreator } from '../use-cases/create/post-creator';
import UserModule from '@/user/store/user-module';
import { Post } from '../common/post';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { PostMutation } from './post-mutation';
import { PostUpdateParams } from '../use-cases/update/post-update-params';
import { PostUpdater } from '../use-cases/update/post-updater';

/**
 * Module for posts
 */
@Module({ namespaced: true, name: 'post' })
export default class PostModule extends VuexModule {
    /**
     * The collection of cached posts.
     */
    public posts: PagedResultSet<Post> | null = null;

    @Mutation
    public [PostMutation.SetPosts](posts: PagedResultSet<Post>) {
        this.posts = posts;
    }

    @Mutation
    public [PostMutation.ClearPosts]() {
        this.posts = null;
    }

    @Mutation
    public [PostMutation.IncrementCommentCount](postId: number) {
        const post = this.posts!.find(p => p.id === postId);

        if (post != null) {
            post.commentCount++;
        }
    }

    /**
     * Create a new post.
     * @param params The post creation parameters.
     */
    @Action
    public async create(params: PostCreateParams) {
        return new PostCreator(this.context.rootGetters['user/authToken']).handle(params);
    }

    /**
     * Update an existing post.
     * @param params The post to update params.
     */
    @Action
    public async update(params: PostUpdateParams) {
        return new PostUpdater(this.context.rootGetters['user/authToken']).handle(params);
    }

    /**
     * Find an active post.
     * @param id The post to look for.
     */
    @Action
    public async findById(id: number) {
        const p = await new PostFinderById(this.context.rootGetters['user/authToken']).handle(id);
        this.context.commit(PostMutation.SetPosts, new PagedResultSet([p], new PaginationInfo(0, 1, 1)));
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    @Action
    public async findByNew(paging: PaginationParams) {
        this.context.commit(PostMutation.ClearPosts);
        const posts = await new PostFinderByNew(this.context.rootGetters['user/authToken']).handle(paging);
        this.context.commit(PostMutation.SetPosts, posts);

        return posts;
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    @Action
    public async findByUser(params: PostFinderByUserParams) {
        this.context.commit(PostMutation.ClearPosts);
        const posts = await new PostFinderByUser(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(PostMutation.SetPosts, posts);

        return posts;
    }
}
