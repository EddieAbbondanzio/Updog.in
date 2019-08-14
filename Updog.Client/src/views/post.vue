<template>
    <master-page>
        <template>
            <post-topic-header :post="post" v-if="post != null" />
        </template>
        <template slot="side-bar">
            <create-post-buttons />
        </template>
        <template slot="footer">FOOTER!</template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import { PostInfo } from '@/post/common/post-info';
import PostTopicHeader from '@/post/components/post-topic-header.vue';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostTopicHeader
    }
})
export default class Post extends PostMixin {
    /**
     * The post being displayed.
     */
    public post: PostInfo | null = null;

    public async created() {
        const postId = Number.parseInt(this.$route.params.id, 10);
        this.post = await this.$findPostById(postId);
    }
}
</script>
