import Vue from 'vue';
import App from './app.vue';
import router from './router';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import VeeVaidate from 'vee-validate';
import store from '@/core/store/store';
import MaterialIcon from '@/core/ui/components/material-icon.vue';
import { capitalize } from './core/ui/filters/capitalize';
import vuetify from './plugins/vuetify';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(VeeVaidate);
Vue.component(MaterialIcon.name, MaterialIcon);

Vue.filter(capitalize.name, capitalize);

const v = new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app');
