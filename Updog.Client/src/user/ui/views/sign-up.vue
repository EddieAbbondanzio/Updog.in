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
import UserRegisterForm from '@/user/ui/components/user-register-form.vue';
import { User } from '@/user';
import { UserLogin } from '@/user';
import Layout from '@/core/ui/components/layout.vue';
import { UserRegistration } from '@/user';

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
