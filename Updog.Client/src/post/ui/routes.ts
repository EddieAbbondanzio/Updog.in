import { RouteConfig } from 'vue-router';

export const postRoutes: RouteConfig[] = [
    {
        path: '/submit',
        name: 'submit',
        component: () => import('@/post/ui/views/submit.vue'),
        meta: {
            authenticate: true
        }
    }
];
