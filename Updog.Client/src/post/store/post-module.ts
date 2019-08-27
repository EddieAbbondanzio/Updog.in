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

/**
 * Module for posts
 */
@Module({ namespaced: true, name: 'post' })
export default class PostModule extends VuexModule {
    /**
     * Create a new post.
     * @param params The post creation parameters.
     */
    @Action
    public async create(params: PostCreateParams) {
        new PostCreator(this.authToken).handle(params);
    }

    /**
     * Find an active post.
     * @param id The post to look for.
     */
    @Action
    public async findById(id: number) {
        new PostFinderById(this.authToken).handle(id);
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    @Action
    public async findByNew(paging: PaginationInfo) {
        new PostFinderByNew(this.authToken).handle(paging);
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    @Action
    public async findByUser(params: PostFinderByUserParams) {
        new PostFinderByUser(this.authToken).handle(params);
    }

    /**
     * Helper to get the auth token of the current user.
     */
    private get authToken() {
        return this.context.rootState.user.userLogin != null ? this.context.rootState.user.userLogin.authToken : '';
    }
}
