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
    if (to.meta.authenticate && store.state.user.userLogin == null) {
        next({
            name: 'login'
        });
    } else {
        next();
    }
});

export default r;
