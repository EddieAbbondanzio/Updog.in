import { ApiInteractor } from '@/core/interactors/api-interactor';
import { UserLogin } from '../../domain/user-login';
import { User } from '../../domain/user';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';
import { UserRegistration } from '@/user/domain/user-registration';

/**
 * Interactor to handle registering a user with the back end.
 */
export class UserRegisterInteractor extends UserApiInteractor<UserRegistration, UserLogin> {
    public async handle(input: UserRegistration): Promise<UserLogin> {
        const response = await this.http.post<UserLogin>('/user/', input);

        return new UserLogin(this.userMapper.map(response.data.user), response.data.authToken);
    }
}
