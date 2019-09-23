import Vuex from 'vuex';
import Vue from 'vue';
import UserStore from '@/user/store/user-store';
import PostStore from '@/post/store/post-store';
import CommentStore from '@/comment/store/comment-store';
import VoteStore from '@/vote/store/vote-store';
import SpaceStore from '@/space/store/space-store';
import { StoreNamespace } from './store-namespace';
import '@/plugins/vuex';

/**
 * Root store.
 */
const s = new Vuex.Store({
    modules: {
        [StoreNamespace.User]: UserStore,
        [StoreNamespace.Post]: PostStore,
        [StoreNamespace.Comment]: CommentStore,
        [StoreNamespace.Vote]: VoteStore,
        [StoreNamespace.Space]: SpaceStore
    }
});

export default s;
