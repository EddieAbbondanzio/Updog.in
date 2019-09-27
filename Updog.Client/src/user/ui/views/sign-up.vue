<template>
    <layout noSideBar="true">
        <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4">
                <v-card>
                    <user-register-form @register="onRegister" />
                </v-card>
            </v-col>
        </v-row>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Layout from '@/core/ui/components/layout.vue';
import UserRegisterForm from '@/user/ui/components/user-register-form.vue';
import { UserLogin } from '@/user/domain/user-login';

@Component({
    components: {
        UserRegisterForm,
        Layout
    }
})
export default class SignUp extends Vue {
    public async onRegister(userLogin: UserLogin) {
        this.$router.push({ name: 'home' });
    }
}
</script>
