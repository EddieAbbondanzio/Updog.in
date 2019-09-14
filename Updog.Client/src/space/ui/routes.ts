import { RouteConfig } from 'vue-router';

export const spaceRoutes: RouteConfig[] = [
    {
        path: '/s/:spaceName',
        name: 'space',
        component: () => import('@/space/ui/views/space.vue')
    }
];
