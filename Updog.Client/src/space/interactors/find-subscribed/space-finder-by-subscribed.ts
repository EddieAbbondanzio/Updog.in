import { SpaceApiInteractor } from '@/space/infrastructure/space-api-interactor';
import { Space } from '@/space/domain/space';

/**
 * Interactor to find the subscribed spaces of the user.
 */
export class SpaceFinderBySubscribed extends SpaceApiInteractor<void, Space[]> {
    public async handle(input: void): Promise<Space[]> {
        const response = await this.http.get<Space[]>('/space/subscribed');
        return response.data.map((s: any) => this.spaceMapper.map(s));
    }
}
