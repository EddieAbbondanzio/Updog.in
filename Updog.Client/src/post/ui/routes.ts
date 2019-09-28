import { RouteConfig } from 'vue-router';
import { commentRoutes } from '@/comment/ui/routes';

export const postRoutes: RouteConfig[] = [
    {
        path: '/submit',
        name: 'submit',
        component: () => import('@/post/ui/views/submit.vue'),
        children: commentRoutes,
        meta: {
            authenticate: true
        }
    }
];
