import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/home.vue';
import { Context } from './core/context';

Vue.use(Router);

const r = new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/login',
            name: 'login',
            component: () => import('./views/login.vue')
        },
        {
            path: '/signup',
            name: 'signup',
            component: () => import('./views/sign-up.vue')
        },
        {
            path: '/submit',
            name: 'submit',
            component: () => import('./views/submit.vue'),
            meta: {
                authenticate: true
            }
        },
        {
            path: '/post/:id',
            component: () => import('./views/post.vue')
        }
    ]
});

r.beforeEach(async (to, from, next) => {
    if (to.meta.authenticate && Context.login == null) {
        next({
            path: '/login'
        });
    } else {
        next();
    }
});

export default r;
