import Mixin from 'vue-class-component';
import Vue from 'vue';
import UserModule from '../store/user-store';
import { getModule } from 'vuex-module-decorators';
import { UserLogin } from '../domain/user-login';

/**
 * Mixin to check if a user is logged in and some
 * helper functions.
 */
@Mixin
export default class AuthenticatedMixin extends Vue {
    /**
     * Get the currently logged in user.
     */
    get $login(): UserLogin | null {
        const userModule: UserModule = getModule(UserModule, this.$store);
        return userModule.userLogin;
    }

    /**
     * Redirect the current page to the login page.
     */
    public $redirectToLogin() {
        this.$router.push({ name: 'login' });
    }

    /**
     * Redirect the current page to the regiser page.
     */
    public $redirectToRegister() {
        this.$router.push({ name: 'signup' });
    }

    /**
     * Check to see if there is a logged in user.
     */
    public $isLoggedIn(): boolean {
        const userModule: UserModule = getModule(UserModule, this.$store);
        return userModule.userLogin != null;
    }
}
