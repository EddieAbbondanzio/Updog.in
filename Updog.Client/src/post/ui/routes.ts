import { RouteConfig } from 'vue-router';
import { commentRoutes } from '@/comment/ui/routes';

export const postRoutes: RouteConfig[] = [
    {
        path: '/submit',
        name: 'submit',
        component: () => import('@/post/ui/views/submit.vue'),
        meta: {
            authenticate: true
        }
    },
    {
        name: 'post',
        path: '/post/:postId',
        component: () => import('@/post/ui/views/post.vue'),
        redirect: '/',
        children: commentRoutes
    }
];
