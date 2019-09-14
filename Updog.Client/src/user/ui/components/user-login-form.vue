<template>
    <b-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <div class="pb-3">
            <h1 class="pb-0 mb-0">Welcome Back!</h1>
            <h5 class="text-muted">
                Not a member?
                <router-link to="/signup">Sign up.</router-link>
            </h5>
        </div>

        <b-form-group>
            <b-form-input
                type="text"
                placeholder="Username"
                v-model="username"
                ref="loginUsernameTextbox"
                name="loginUsername"
                v-validate="'required'"
                @keyup.enter="onLogin"
            />
            <b-form-invalid-feedback
                class="d-block text-left"
                :state="false"
            >{{ errors.first('loginUsername')}}</b-form-invalid-feedback>
        </b-form-group>
        <b-form-group>
            <b-form-input
                type="password"
                placeholder="Password"
                v-model="password"
                name="loginPassword"
                v-validate="'required'"
                @keyup.enter="onLogin"
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
import { UserAuthMixin, UserCredentials } from '@/user';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form'
})
export default class UserLoginForm extends UserAuthMixin {
    public $refs!: {
        loginUsernameTextbox: HTMLInputElement;
    };

    /**
     * Username entered by the user.
     */
    public username: string = '';

    /**
     * Password entered by the user.
     */
    public password: string = '';

    /**
     * Set up the error messages when the component is created.
     */
    public created() {
        this.$validator.localize('en', {
            custom: {
                loginUsername: {
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
     * When the form is rendered, auto focus the username textbox.
     */
    public mounted() {
        this.$refs.loginUsernameTextbox.focus();
    }

    /**
     * Attempt to log in the user.
     */
    public async onLogin() {
        // Validate first.
        if (!(await this.$validator.validate())) {
            return;
        }

        // Send off the request to the backend.
        const login = await this.$loginUser(new UserCredentials(this.username, this.password));

        this.$emit('login', login);
    }

    /**
     * Reset the form back to defaults.
     */
    public onReset() {
        this.username = '';
        this.password = '';
        this.$validator.reset();
    }
}
</script>
