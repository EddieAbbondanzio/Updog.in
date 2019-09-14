import { RouteConfig } from 'vue-router';
import Home from '@/core/ui/views/home.vue';

export const coreRoutes: RouteConfig[] = [
    {
        path: '/',
        name: 'home',
        component: Home
    }
];
