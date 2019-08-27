import Mixin from 'vue-class-component';
import UserModule from '../store/user-module';
import { getModule } from 'vuex-module-decorators';
import { UserCredentials } from '../common/user-credentials';
import { UserLogin } from '../common/user-login';
import Vue from 'vue';
import { User } from '../common/user';
import { UserRegistration } from '../common/user-registration';

/**
 * Mixin to find users in the backend.
 */
@Mixin
export class UserAuthMixin extends Vue {
    /**
     * The Vuex store module for user data.
     */
    private userModule: UserModule = getModule(UserModule, this.$store);

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
        return this.userModule.userLogin != null;
    }

    /**
     * Get the currently logged in user.
     */
    get $login(): UserLogin | null {
        return this.userModule.userLogin;
    }

    /**
     * Log in the user with the backend.
     * @param userCreds The username / password combo to log in with.
     */
    public async $loginUser(userCreds: UserCredentials): Promise<UserLogin> {
        await this.userModule.login(userCreds);
        return this.$login!;
    }

    /**
     * Log out the active user.
     */
    public async $logoutUser(): Promise<void> {
        if (!this.$isLoggedIn()) {
            throw new Error('Cannot log out. No logged in user');
        }

        await this.userModule.logout();
    }

    /**
     * Log in the user with the backend.
     * @param userCreds The username / password combo to log in with.
     */
    public async $registerUser(userReg: UserRegistration): Promise<UserLogin> {
        await this.userModule.register(userReg);
        return this.$login!;
    }
}
