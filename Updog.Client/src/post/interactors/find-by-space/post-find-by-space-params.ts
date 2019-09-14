import { PaginationParams } from '@/core/pagination/pagination-params';

export class PostFindBySpaceParams {
    constructor(public space: string, public pagination: PaginationParams) {}
}
