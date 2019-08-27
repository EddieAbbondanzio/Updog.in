<template>
    <master-page noSideBar="true">
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-login-form @login="onLogin" />
                </b-col>
            </b-row>
        </b-container>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/components/user-login-form.vue';
import { UserLogin } from '../user/common/user-login';
import { User } from '../user/common/user';
import MasterPage from '@/core/components/master-page.vue';
import { UserCredentials } from '../user/common/user-credentials';

@Component({
    components: {
        UserLoginForm,
        MasterPage
    }
})
export default class Login extends Vue {
    public async onLogin(login: UserLogin) {
        this.$router.push({ name: 'home' });
        // EventBus.emit('login', login);
    }
}
</script>
