import { RouteConfig } from 'vue-router';
import Component from 'vue-class-component';
import { commentRoutes } from '@/comment/ui/routes';

export const spaceRoutes: RouteConfig[] = [
    {
        path: '/s/:spaceName',
        name: 'space',
        redirect: { name: 'spaceNew' },
        component: () => import('@/space/ui/views/space.vue'),
        children: [
            {
                path: 'new',
                name: 'spaceNew',
                component: () => import('@/space/ui/views/new-posts.vue')
            },
            {
                name: 'post',
                path: 'post/:postId',
                component: () => import('@/post/ui/views/post.vue'),
                redirect: { name: 'comments' },
                children: commentRoutes
            },
            {
                name: 'spaceSubmit',
                path: 'submit',
                component: () => import('@/space/ui/views/submit.vue'),
                meta: {
                    authenticate: true
                }
            }
        ]
    }
];
