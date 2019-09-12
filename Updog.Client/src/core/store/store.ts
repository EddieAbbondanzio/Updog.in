import Vuex from 'vuex';
import Vue from 'vue';
import UserModule from '@/user/store/user-module';
import PostModule from '@/post/store/post-module';
import { UserLogin } from '@/user/domain/user-login';
import CommentModule from '@/comment/store/comment-module';
import VoteModule from '@/vote/store/vote-module';

Vue.use(Vuex);

/**
 * Root store.
 */
export default new Vuex.Store({
    modules: {
        user: UserModule,
        post: PostModule,
        comment: CommentModule,
        vote: VoteModule
    }
});
