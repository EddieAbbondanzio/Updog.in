<template>
    <b-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <h1 class="pb-3">Welcome Back!</h1>
        <b-form-group>
            <b-form-input
                type="email"
                placeholder="Email"
                v-model="loginEmail"
                name="loginEmail"
                v-validate="'required|email'"
            />
            <b-form-invalid-feedback
                class="d-block text-left"
                :state="false"
            >{{ errors.first('loginEmail')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="password"
                placeholder="Password"
                v-model="loginPassword"
                name="loginPassword"
                v-validate="'required'"
            />
            <b-form-invalid-feedback
                class="d-block text-left"
                :state="false"
            >{{ errors.first('loginPassword')}}</b-form-invalid-feedback>
        </b-form-group>

        <b-form-group class="form-buttons pt-3">
            <b-button variant="primary" @click="onLogin">Login</b-button>
            <b-button variant="outline-primary" @click="onReset">Reset</b-button>
        </b-form-group>
    </b-form>
</template>

<style scoped>
.form-buttons button {
    margin-left: 8px;
    margin-right: 8px;
}
</style>


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form',
    components: {}
})
export default class UserLoginForm extends Vue {
    public loginEmail: string = '';

    public loginPassword: string = '';

    public created() {
        this.$validator.localize('en', {
            custom: {
                loginEmail: {
                    required: 'Email is required',
                    email: 'Email must be a valid address.'
                },
                loginPassword: {
                    required: 'Password is required'
                }
            }
        });
    }

    /**
     * Attempt to log in the user.
     */
    public async onLogin() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }
    }

    public onReset() {
        this.loginEmail = '';
        this.loginPassword = '';
        this.$validator.reset();
    }
}
</script>
