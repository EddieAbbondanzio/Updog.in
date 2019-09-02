import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/home.vue';
import store from '@/core/store/store';

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
            name: 'post',
            path: '/post/:postId',
            component: () => import('./views/post.vue'),
            children: [
                { name: 'comments', path: 'comments', component: () => import('./views/comments.vue') },
                { name: 'comment', path: 'comment/:commentId', component: () => import('./views/comment.vue') }
            ]
        },
        {
            path: '/user/:username',
            component: () => import('./views/user.vue')
        }
    ]
});

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
