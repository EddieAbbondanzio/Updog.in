import Vuex from 'vuex';
import Vue from 'vue';
import UserModule from '@/user/store/user-module';
import PostModule from '@/post/store/post-module';
import { UserLogin } from '@/user/common/user-login';

Vue.use(Vuex);

/**
 * Root store.
 */
export default new Vuex.Store({
    modules: {
        user: UserModule,
        post: PostModule
    }
});
