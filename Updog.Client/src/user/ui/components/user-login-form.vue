<template>
    <b-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <div class="pb-3">
            <h1 class="pb-0 mb-0">Welcome Back!</h1>
            <h5 class="text-muted">
                Not a member?
                <router-link to="/signup">Sign up.</router-link>
            </h5>
        </div>

        <b-alert
            variant="danger"
            :show="loginFailed"
            dismissible
            @dismissed="loginFailed = false"
        >{{ serverErrorMessage}}</b-alert>

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
            <b-button variant="primary" @click="submit">Login</b-button>
            <b-button variant="outline-primary" @click="reset">Reset</b-button>
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
import { UserLoginMixin, UserCredentials, UserLogin } from '@/user';
import { Form } from '@/core';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form'
})
export default class UserLoginForm extends UserLoginMixin implements Form<UserLogin | null> {
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
     * If a failed login attempt occured.
     */
    public loginFailed: boolean = false;

    /**
     * Error message from the server if the login failed.
     */
    public serverErrorMessage: string = '';

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
    public async submit() {
        try {
            if (!(await this.$validator.validate())) {
                return null;
            }

            const login = await this.$loginUser(new UserCredentials(this.username, this.password));

            this.$emit('submit', login);
            return login;
        } catch (error) {
            // Unauthorized return means login failed.
            this.loginFailed = error.response.status === 401;
            this.serverErrorMessage = error.response.data;

            return null;
        }
    }

    /**
     * Reset the form back to defaults.
     */
    public reset() {
        this.username = '';
        this.password = '';
        this.loginFailed = false;
        this.serverErrorMessage = '';
        this.$validator.reset();
    }
}
</script>
