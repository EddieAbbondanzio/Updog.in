<template>
    <layout noSideBar="true">
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-login-form @login="onLogin" />
                </b-col>
            </b-row>
        </b-container>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/components/user-login-form.vue';
import { UserLogin } from '../user/common/user-login';
import { User } from '../user/common/user';
import Layout from '@/core/components/layout.vue';
import { UserCredentials } from '../user/common/user-credentials';

@Component({
    components: {
        UserLoginForm,
        Layout
    }
})
export default class Login extends Vue {
    public async onLogin(login: UserLogin) {
        this.$router.push({ name: 'home' });
    }
}
</script>
