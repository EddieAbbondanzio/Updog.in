import { UserCredentials } from './user-credentials';
import { User } from './user';
import { ApiService } from '@/common/api-service';

export class UserService extends ApiService {
    /**
     * Log into an existing user's account.
     * @param creds The credentials to authenticate.
     */
    public async login(creds: UserCredentials) {}

    public async register() {}

    public async update() {}

    public async updatePassword() {}
}
