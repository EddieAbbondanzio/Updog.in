<template>
    <div>hi</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../post/common/post';
import { PostMixin } from '../post/mixins/post-mixin';
import { PaginationInfo } from '../core/pagination-info';

/**
 * User details page.
 */
@Component({
    name: 'user'
})
export default class User extends PostMixin {
    /**
     * The ID of the user.
     */
    public username: string = '';

    /**
     * Posts the user has made.
     */
    public posts: Post[] = [];

    public async created() {
        this.username = this.$route.params.username;
        this.posts = await this.$findPostsByUser(this.username, new PaginationInfo(0, Post.DEFAULT_PAGE_SIZE));
    }
}
</script>