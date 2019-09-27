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
import { Space as SpaceEntity } from '@/space/domain/space';
import { Post } from '@/post/domain/post';
import SpaceViewerMixin from '@/space/mixins/space-viewer-mixin';
import { PostFindBySpaceParams } from '@/post/interactors/find-by-space/post-find-by-space-params';
import Layout from '@/core/ui/components/layout.vue';
import SpaceLink from '@/space/ui/components/space-link.vue';
import CreatePostButtons from '@/post/ui/components/create-post-buttons.vue';

/**
 * Page to view a space and it's posts.
 */
@Component({
    name: 'space',
    components: {
        Layout,
        SpaceLink,
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