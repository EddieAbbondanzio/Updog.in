import Vue from 'vue';
import Router from 'vue-router';
import store from '@/core/store/store';
import { postRoutes } from './post/ui/routes';
import { userRoutes } from './user/ui/routes';
import { spaceRoutes } from './space/ui/routes';
import { coreRoutes } from './core/ui/routes';

Vue.use(Router);

const r = new Router({
    routes: [...coreRoutes, ...spaceRoutes, ...userRoutes, ...postRoutes]
});

r.mode = 'hash';

r.beforeEach(async (to, from, next) => {
    // Page should only allow logged in users.
    if (to.meta.authenticate) {
        if (store.state.user.userLogin == null) {
            next({ name: 'login' });
        } else {
            next();
        }
        // Page should not allow any logged in users.
    } else if (to.meta.anonymous) {
        if (store.state.user.userLogin == null) {
            next();
        } else {
            next({ name: 'home' });
        }
        // Regular page
    } else {
        next();
    }
});

export default r;
