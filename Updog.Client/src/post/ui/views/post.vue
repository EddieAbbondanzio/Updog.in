<template>
    <div v-if="isLoaded">
        <post-summary :post="$posts[0]" :showEdit="true" :showToggle="false" expand="true" />

        <!-- Comments! -->
        <router-view></router-view>
    </div>
    <div v-else>
        <post-loading-place-holder />
    </div>
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
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { PostFinderMixin } from '@/post';
import PostLoadingPlaceHolder from '@/post/ui/components/post-loading-placeholder.vue';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        PostSummary,
        PostLoadingPlaceHolder
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
