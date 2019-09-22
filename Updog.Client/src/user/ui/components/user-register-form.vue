<template>
    <v-form class="bg-light border px-5 pt-5 pb-3 my-5">
        <div class="pb-3">
            <h1 class="pb-0 mb-0">Sign Up</h1>
            <h5 class="text-muted">
                Already a member?
                <router-link to="/login">Sign in.</router-link>
            </h5>
        </div>
        <v-text-field
            placeholder="Username"
            v-model="username"
            ref="registerUsernameTextbox"
            name="registerUsername"
            v-validate="'required|min:4|isUnique'"
            title="Username must be at least 4 characters"
            :error="errors.first('registerUsername') != null"
            :error-messages="errors.first('registerUsername')"
        />
        <v-text-field
            type="email"
            placeholder="Email"
            v-model="email"
            name="registerEmail"
            v-validate="'email'"
            :error="errors.first('registerEmail') != null"
            :error-messages="errors.first('registerEmail')"
        />

        <v-text-field v-show="false" type="text" v-model="honey" placeholder="Name" />
        <v-text-field
            type="password"
            placeholder="Password must be at least 8 characters."
            v-model="password"
            ref="registerPassword"
            name="registerPassword"
            v-validate="'required'"
            :error="errors.first('registerPassword') != null"
            :error-messages="errors.first('registerPassword')"
        />

        <v-text-field
            type="password"
            placeholder="Confirm password"
            v-model="confirmPassword"
            name="registerConfirmPassword"
            v-validate="'required|confirmed:registerPassword'"
            :error="errors.first('registerConfirmPassword') != null"
            :error-messages="errors.first('registerConfirmPassword')"
        />

        <v-btn class="mr-2" color="primary" @click="submit">Sign Up</v-btn>
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
import { Component, Vue, Mixins } from 'vue-property-decorator';
import { UserRegistration } from '@/user/domain/user-registration';
import { UserFinderMixin } from '../../mixins/user-finder-mixin';
import { mixins } from 'vue-class-component';
import { UserRegistrarMixin } from '@/user/mixins/user-registrar-mixin';
import { Form } from '../../../core';
import { UserLogin } from '../../domain/user-login';

/**
 * Login form for logging in users via username / password.
 */
@Component({
    name: 'user-login-form'
})
export default class UserRegisterForm extends UserRegistrarMixin implements Form<UserLogin | null> {
    public $refs!: {
        registerUsernameTextbox: HTMLInputElement;
    };

    public username: string = '';
    public email: string = '';
    public password: string = '';
    public confirmPassword: string = '';
    public honey: string = '';

    public created() {
        // Add way to check the backend for available username.
        this.$validator.extend('isUnique', async value => this.$isUsernameAvailable(value));

        this.$validator.localize('en', {
            custom: {
                registerUsername: {
                    required: 'Username is required.',
                    min: 'Username must be at least 4 characters.',
                    isUnique: 'Username is unavailable.'
                },
                registerEmail: {
                    email: 'Email must be a valid address.'
                },
                registerPassword: {
                    required: 'Password is required',
                    min: 'Password must be at least 8 characters.'
                },
                registerConfirmPassword: {
                    required: 'Confirm password is required',
                    confirmed: 'Passwords do not match.'
                }
            }
        });
    }

    public mounted() {
        this.$refs.registerUsernameTextbox.focus();
    }

    /**
     * Attempt to log in the user.
     */
    public async submit() {
        if (this.honey !== '') {
            throw new Error('Oh bother');
        }

        // Validate first.
        if (!(await this.$validator.validate())) {
            return null;
        }

        const login = await this.$registerUser(new UserRegistration(this.username, this.password, this.email));

        this.$emit('register', login);
        return login;
    }

    /**
     * Reset the form back to initial state.
     */
    public reset() {
        this.username = '';
        this.email = '';
        this.password = '';
        this.confirmPassword = '';
        this.$validator.reset();
    }
}
</script>
