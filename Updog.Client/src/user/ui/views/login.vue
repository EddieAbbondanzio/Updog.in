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
import UserLoginForm from '@/user/ui/components/user-login-form.vue';
import Layout from '@/core/ui/components/layout.vue';
import { UserLogin } from '@/user';

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
