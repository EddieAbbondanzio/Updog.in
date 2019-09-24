<template>
    <layout>
        <template>
            <router-view></router-view>
        </template>
        <template slot="side-bar">
            <v-card v-if="space != null" class="pa-3">
                <v-card-title>
                    <space-link :space="space" />
                </v-card-title>

                <v-card-text>
                    <p>{{ space.description }}</p>
                </v-card-text>

                <v-card-actions>
                    <create-post-buttons />
                </v-card-actions>
            </v-card>
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
import CreatePostButtons from '@/post/ui/components/create-post-buttons.vue';

/**
 * Page to view a space and it's posts.
 */
@Component({
    name: 'space',
    components: {
        Layout,
        SpaceLink,
        PostSummaryList,
        CreatePostButtons
    }
})
export default class Space extends SpaceViewerMixin {
    public space: SpaceEntity | null = null;

    public async created() {
        this.space = await this.$findSpace(this.$route.params.spaceName);
    }
}
</script>