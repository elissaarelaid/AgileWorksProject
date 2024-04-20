import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';
import Application from '@/views/Application.vue'
import ApplicationForm from '@/views/ApplicationForm.vue';

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Applications', 
        component: Application,
        props: { title: 'Applications' },
        meta: { requiresAuth: false },
    },
    {
        path: '/add',
        name: 'ApplicationForm', 
        component: ApplicationForm,
        props: { title: 'Application Form' },
        meta: { requiresAuth: false },
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
  });

export default router;