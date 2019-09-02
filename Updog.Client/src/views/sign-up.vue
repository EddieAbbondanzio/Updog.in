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
import UserRegisterForm from '@/user/components/user-register-form.vue';
import { User } from '@/user/common/user';
import { UserLogin } from '@/user/common/user-login';
import Layout from '@/core/components/layout.vue';
import { UserRegistration } from '@/user/common/user-registration';

@Component({
    components: {
        UserRegisterForm,
        Layout
    }
})
export default class SignUp extends Vue {
    public async onRegister(userLogin: UserLogin) {
        this.$router.push({ name: 'home' });

        // EventBus.emit('login', userLogin);
    }
}
</script>
