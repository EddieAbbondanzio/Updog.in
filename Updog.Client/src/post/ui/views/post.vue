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
import CreatePostButtons from '@/post/ui/components/create-post-buttons.vue';
import Layout from '@/core/ui/components/layout.vue';
import PostSummary from '@/post/ui/components/post-summary.vue';
import { CommentFinderMixin } from '@/comment';
import { CommentCreatorMixin } from '@/comment';
import { mixins } from 'vue-class-component/lib/util';
import { Post as PostEntity } from '@/post';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import { User } from '@/user';
import { UserAuthMixin } from '@/user';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import PostVoteBar from '@/vote/ui/components/post-vote-bar.vue';
import { PostFinderMixin } from '@/post';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        Layout,
        PostSummary,
        CommentSummary,
        PaginationNav,
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
