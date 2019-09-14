import Vue from 'vue';
import Router from 'vue-router';
import Home from '@/core/ui/views/home.vue';
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
            component: () => import('@/user/ui/views/login.vue')
        },
        {
            path: '/signup',
            name: 'signup',
            component: () => import('@/user/ui/views/sign-up.vue')
        },
        {
            path: '/submit',
            name: 'submit',
            component: () => import('@/post/ui/views/submit.vue'),
            meta: {
                authenticate: true
            }
        },
        {
            path: '/s/:spaceName',
            name: 'space',
            component: () => import('@/space/ui/views/space.vue')
        },
        {
            name: 'post',
            path: '/post/:postId',
            component: () => import('@/post/ui/views/post.vue'),
            redirect: '/',
            children: [
                { name: 'comments', path: 'comments', component: () => import('@/comment/ui/views/comments.vue') },
                {
                    name: 'comment',
                    path: 'comment/:commentId',
                    component: () => import('@/comment/ui/views/comment.vue')
                }
            ]
        },
        {
            name: 'user',
            path: '/u/:username',
            component: () => import('@/user/ui/views/user.vue')
        }
    ]
});

r.mode = 'history';

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
