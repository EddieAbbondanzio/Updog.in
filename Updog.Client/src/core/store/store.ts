import Vuex from 'vuex';
import Vue from 'vue';
import UserModule from '@/user/store/user-module';

Vue.use(Vuex);

/**
 * Root store.
 */
export default new Vuex.Store({
    modules: {
        user: UserModule
    }
});
