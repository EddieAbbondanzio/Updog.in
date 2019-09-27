<template>
    <layout noSideBar="true">
        <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4">
                <v-card>
                    <user-login-form @submit="onLogin" />
                </v-card>
            </v-col>
        </v-row>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginForm from '@/user/ui/components/user-login-form.vue';
import Layout from '@/core/ui/components/layout.vue';
import { UserLogin } from '@/user/domain/user-login';

@Component({
    components: {
        UserLoginForm,
        Layout
    }
})
export default class Login extends Vue {
    public async onLogin(login: UserLogin) {
        this.$router.back();
    }
}
</script>
