import { UserCredentials } from './common/user-credentials';
import { User } from './common/user';
import { ApiService } from '@/common/api-service';
import { UserRegistration } from './common/user-registration';
import { UserLogin } from './common/user-login';

export class UserService extends ApiService {
    /**
     * Log into an existing user's account.
     * @param creds The credentials to authenticate.
     */
    public async login(creds: UserCredentials): Promise<UserLogin> {
        throw new Error();
    }

    /**
     * Register a new user.
     * @param reg The registration info.
     */
    public async register(reg: UserRegistration): Promise<UserLogin> {
        const res = await this.http.post(`${this.backendUrl}/user/`, reg);
        console.log(res);
        throw new Error();
    }

    public async update() {
        throw new Error();
    }

    public async updatePassword() {
        throw new Error();
    }
}
