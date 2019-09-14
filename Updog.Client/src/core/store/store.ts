import Vuex from 'vuex';
import Vue from 'vue';
import UserStore from '@/user/store/user-store';
import PostStore from '@/post/store/post-store';
import CommentStore from '@/comment/store/comment-store';
import VoteStore from '@/vote/store/vote-store';
import SpaceStore from '@/space/store/space-store';
import { StoreName } from './store-name';

Vue.use(Vuex);

/**
 * Root store.
 */
export default new Vuex.Store({
    modules: {
        [StoreName.User]: UserStore,
        [StoreName.Post]: PostStore,
        [StoreName.Comment]: CommentStore,
        [StoreName.Vote]: VoteStore,
        [StoreName.Space]: SpaceStore
    }
});
