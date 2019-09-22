<template>
    <layout noSideBar="true">
        <b-container>
            <b-row>
                <b-col md="8" lg="6" offset-md="2" offset-lg="3">
                    <user-register-form @register="onRegister" />
                </b-col>
            </b-row>
        </b-container>
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
