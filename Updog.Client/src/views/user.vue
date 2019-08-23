<template>
    <master-page>
        <template>REEE</template>
        <template slot="side-bar">
            <div class="bg-light border p-3">
                <h4>{{ user.username}}</h4>
            </div>
        </template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import { Post } from '../post/common/post';
import { PostMixin } from '../post/mixins/post-mixin';
import { PaginationInfo } from '../core/pagination-info';
import MasterPage from '@/core/components/master-page.vue';
import { UserMixin } from '@/user/mixins/user-mixin';
import { User as UserEntity } from '@/user/common/user';

/**
 * User details page.
 */
@Component({
    name: 'user',
    components: {
        MasterPage
    },
    mixins: [UserMixin, PostMixin]
})
export default class User extends Mixins(UserMixin, PostMixin) {
    /**
     * The ID of the user.
     */
    public user!: UserEntity;

    /**
     * Posts the user has made.
     */
    public posts: Post[] = [];

    public async created() {
        const username = this.$route.params.username;
        this.user = await this.$findUserByUsername(username);

        this.posts = await this.$findPostsByUser(username, new PaginationInfo(0, Post.DEFAULT_PAGE_SIZE));
    }

    public memberLength() {
        return this;
    }
}
</script>