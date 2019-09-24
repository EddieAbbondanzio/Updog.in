import Vue from 'vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify);

export default new Vuetify({
    icons: {
        iconfont: 'mdi'
    },
    theme: {
        themes: {
            light: {
                primary: '#039BE5',
                secondary: '#FFC107',
                accent: '#03A9F4',
                error: '#b71c1c'
            }
        }
    }
});
