import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';
import { User } from '@/user/domain/user';
import { UserMapper } from '@/user/infrastructure/user-mapper';

/**
 * API interactor to find posts by the user that created them.
 */
export class UserFinderByUsername extends UserApiInteractor<string, User> {
    public async handle(input: string): Promise<User> {
        const response = await this.http.get<User>(`/user/${input}`);
        return new UserMapper().map(response.data);
    }
}
