import Mixin from 'vue-class-component';
import UserModule from '../store/user-module';
import { getModule } from 'vuex-module-decorators';
import { UserCredentials } from '../common/user-credentials';
import { UserLogin } from '../common/user-login';
import Vue from 'vue';
import { UserRegistration } from '../common/user-registration';

/**
 * Mixin to register a user with the backend.
 */
@Mixin
export class UserRegisterMixin extends Vue {
    /**
     * The Vuex store module for user data.
     */
    private userModule: UserModule = getModule(UserModule, this.$store);

    /**
     * Log in the user with the backend.
     * @param userCreds The username / password combo to log in with.
     */
    public $register(userReg: UserRegistration): Promise<UserLogin> {
        return this.userModule.register(userReg);
    }
}
