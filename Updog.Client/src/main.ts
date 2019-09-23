import Vue from 'vue';
import router from './router';
import VeeVaidate from 'vee-validate';
import store from '@/core/store/store';
import { capitalize } from './core/ui/filters/capitalize';
import vuetify from './plugins/vuetify';
import 'vuetify/dist/vuetify.min.css';

Vue.config.productionTip = false;
Vue.use(VeeVaidate);

Vue.filter(capitalize.name, capitalize);

/*
 * Hack. Don't friggen even think about moving this, lest ye
 * desires a rawModule is undefined error.
 */
import App from './app.vue';

const v = new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app');
