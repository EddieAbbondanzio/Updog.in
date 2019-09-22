<template>
    <layout>
        <template>
            <post-summary-list :posts="posts" />
        </template>
        <template slot="side-bar">
            <div class="bg-light border p-3" v-if="space != null">
                <h3>
                    <space-link :space="space" variant="dark" />
                </h3>
                <p>{{ space.description }}</p>
            </div>
        </template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from 'vue-property-decorator';
import { SpaceViewerMixin } from '@/space/mixins/space-viewer-mixin';
import Layout from '@/core/ui/components/layout.vue';
import { Space as SpaceEntity } from '@/space/domain/space';
import SpaceLink from '@/space/ui/components/space-link.vue';
import PostSummaryList from '@/post/ui/components/post-summary-list.vue';
import { Post } from '@/post/domain/post';
import { PagedResultSet, PaginationParams } from '@/core';
import { PostFindBySpaceParams } from '@/post';

/**
 * Page to view a space and it's posts.
 */
@Component({
    name: 'space',
    components: {
        Layout,
        SpaceLink,
        PostSummaryList
    }
})
export default class Space extends SpaceViewerMixin {
    public space: SpaceEntity | null = null;

    public posts: PagedResultSet<Post> | null = null;

    public async created() {
        this.refreshPosts();
    }

    @Watch('$route')
    public async watchRoute() {
        this.refreshPosts();
    }

    private async refreshPosts() {
        this.space = await this.$findSpace(this.$route.params.spaceName);
        this.posts = await this.$findPosts(new PostFindBySpaceParams(this.space.name, new PaginationParams(0, 20)));
    }
}
</script>