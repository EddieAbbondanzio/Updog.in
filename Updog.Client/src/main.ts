import Vue from 'vue';
import App from './app.vue';
import router from './router';
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import VeeVaidate from 'vee-validate';
import Vuex from 'vuex';
import store from '@/core/store/store';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(VeeVaidate);

const v = new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
