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
import { Post as PostEntity } from '@/post/domain/post';
import { User } from '@/user/domain/user';
import PostFinderMixin from '@/post/mixins/post-finder-mixin';
import PostSummary from '@/post/ui/components/post-summary.vue';
import PostLoadingPlaceHolder from '@/post/ui/components/post-loading-place-holder.vue';

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
