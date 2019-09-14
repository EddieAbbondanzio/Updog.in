import { RouteConfig } from 'vue-router';

export const commentRoutes: RouteConfig[] = [
    { name: 'comments', path: 'comments', component: () => import('@/comment/ui/views/comments.vue') },
    {
        name: 'comment',
        path: 'comment/:commentId',
        component: () => import('@/comment/ui/views/comment.vue')
    }
];
