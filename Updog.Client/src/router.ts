import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/home.vue';

Vue.use(Router);

export default new Router({
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
        }
    ]
});
