import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import 'virtual:windi.css'
import { setApiUrl } from './modules/api'
import { createPinia } from 'pinia';
import router from './router/router';

setApiUrl('https://localhost:7220'); 

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.mount('#app')
