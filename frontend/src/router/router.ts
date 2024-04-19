import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import Applcation from '@/views/Application.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Applications', 
        component: Applcation,
        props: { title: 'Applications' },
        meta: { requiresAuth: false },
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
  });

export default router;