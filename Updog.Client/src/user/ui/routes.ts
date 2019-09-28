import { RouteConfig } from 'vue-router';

export const userRoutes: RouteConfig[] = [
    {
        path: '/login',
        name: 'login',
        component: () => import('@/user/ui/views/login.vue')
    },
    {
        path: '/signup',
        name: 'signup',
        component: () => import('@/user/ui/views/sign-up.vue'),
        meta: {
            anonymous: true
        }
    },
    {
        name: 'user',
        path: '/u/:username',
        component: () => import('@/user/ui/views/user.vue')
    }
];
