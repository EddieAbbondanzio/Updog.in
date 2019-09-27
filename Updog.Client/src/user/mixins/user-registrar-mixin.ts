import Mixin from 'vue-class-component';
import AuthenticatedMixin from './authenticated-mixin';
import { getModule } from 'vuex-module-decorators';
import UserStore from '../store/user-store';
import { UserRegistration } from '../domain/user-registration';
import { UserLogin } from '../domain/user-login';

/**
 * Mixin to handle registering new users.
 */
@Mixin
export default class UserRegistrarMixin extends AuthenticatedMixin {
    /**
     * Check to see if a username is available.
     * @param username The username to check.
     */
    public async $isUsernameAvailable(username: string): Promise<boolean> {
        const userModule = getModule(UserStore, this.$store);
        return userModule.isUsernameAvailable(username);
    }

    /**
     * Log in the user with the backend.
     * @param userCreds The username / password combo to log in with.
     */
    public async $registerUser(userReg: UserRegistration): Promise<UserLogin> {
        const userModule = getModule(UserStore, this.$store);
        await userModule.register(userReg);
        return this.$login!;
    }
}
