<template>
    <layout>
        <template>
            <div v-if="isLoaded">
                <post-summary :post="$posts[0]" :showEdit="true" :showToggle="false" expand="true" />

                <!-- Comments! -->
                <router-view></router-view>
            </div>
        </template>

        <!-- Side Bar -->
        <template slot="side-bar">
            <create-post-buttons />

            <div v-if="isLoaded">
                <post-vote-bar :post="$posts[0]" />
            </div>
        </template>
        <template slot="footer">FOOTER!</template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue, Mixins } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import Layout from '@/core/components/layout.vue';
import { PostFinderMixin } from '../post/mixins/post-finder-mixin';
import PostSummary from '@/post/components/post-summary.vue';
import { CommentFinderMixin } from '@/comment/mixins/comment-finder-mixin';
import { CommentCreatorMixin } from '@/comment/mixins/comment-creator-mixin';
import { mixins } from 'vue-class-component/lib/util';
import { Post as PostEntity } from '@/post/domain/post';
import CommentSummary from '@/comment/components/comment-summary.vue';
import { Comment } from '../comment/domain/comment';
import User from './user.vue';
import { UserAuthMixin } from '@/user/mixins/user-auth-mixin';
import { CommentCreateParams } from '../comment/use-cases/create/comment-create-params';
import { CommentFinderByPostParams } from '../comment/use-cases/find-by-post/comment-finder-by-post-params';
import { PaginationParams } from '../core/pagination/pagination-params';
import { PagedResultSet } from '../core/pagination/paged-result-set';
import PaginationNavigation from '@/core/components/pagination-navigation.vue';
import PostVoteBar from '@/vote/components/post-vote-bar.vue';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        Layout,
        PostSummary,
        CommentSummary,
        PaginationNavigation,
        PostVoteBar
    }
})
export default class Post extends PostFinderMixin {
    public isLoaded = false;

    get postId() {
        return Number.parseInt(this.$route.params.postId, 10);
    }

    public async created() {
        const p = await this.$findPostById(this.postId);
        this.isLoaded = true;
    }
}
</script>
