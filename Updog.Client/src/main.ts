import Vue from 'vue';
import router from './router';
import store from '@/core/store/store';
import { capitalize } from './core/ui/filters/capitalize';
import vuetify from './plugins/vuetify';
import './plugins/vue-content-placeholders';
import './plugins/vee-validate';
import App from './app.vue';
import { shortHand } from '@/core/ui/filters/short-hand';

Vue.config.productionTip = false;

Vue.filter('capitalize', capitalize);
Vue.filter('shortHand', shortHand);

const v = new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app');
