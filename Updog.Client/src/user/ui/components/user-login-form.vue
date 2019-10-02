<template>
    <v-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <div class="pb-3">
            <h1 class="pb-0 mb-0">Welcome Back!</h1>
            <span class="grey--text subtitle-1">
                Not a member?
                <router-link to="/signup">Sign up.</router-link>
            </span>
        </div>

        <v-alert type="error" v-model="loginFailed" dismissible>{{ serverErrorMessage}}</v-alert>

        <v-text-field
            placeholder="Username"
            v-model="username"
            ref="loginUsernameTextbox"
            name="loginUsername"
            v-validate="'required'"
            @keyup.enter="submit"
            :error="errors.first('loginUsername') != null ? true : false"
            :error-messages="errors.first('loginUsername')"
        />
        <v-text-field
            type="password"
            placeholder="Password"
            v-model="password"
            name="loginPassword"
            v-validate="'required'"
            @keyup.enter="submit"
            :error="errors.first('loginPassword') != null ? true : false"
            :error-messages="errors.first('loginPassword')"
        />

        <v-switch v-model="rememberMe" label="Remember Me" />

        <v-btn class="mr-2" color="primary" @click="submit">Login</v-btn>
        <v-btn class="ml-2" color="error" outlined @click="reset">Reset</v-btn>
    </v-form>
</template>

<style scoped>
.form-buttons button {
    margin-left: 8px;
    margin-right: 8px;
}
</style>


<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import UserLoginMixin from '@/user/mixins/user-login-mixin';
import Cookie from 'js-cookie';
import { Form } from '@/core/ui/common/form/form';
import { UserCredentials } from '@/user/domain/user-credentials';
import { HttpStatusCode } from '@/core/common/http-status-code';
import { UserLogin } from '@/user/domain/user-login';

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
     * If the user info should be saved off.
     */
    public rememberMe: boolean = false;

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

            // Avenge me!
            if (this.rememberMe) {
                Cookie.set('auth', login.authToken, { secure: true });
            }

            return login;
        } catch (error) {
            // Unauthorized return means login failed.
            this.loginFailed = error.response.status === HttpStatusCode.Unauthorized;
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
