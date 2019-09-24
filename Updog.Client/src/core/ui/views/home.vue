<template>
    <layout>
        <template>
            <post-summary-list :posts="$posts" />
            <pagination-nav
                v-if="$posts != null"
                :pagination="$posts.pagination"
                @previous="onPrevious"
                @next="onNext"
            />
        </template>
        <template slot="side-bar">
            <v-card class="pa-3">
                <v-card-actions>
                    <create-post-buttons />
                </v-card-actions>
            </v-card>
        </template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Layout from '@/core/ui/components/layout.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { getModule } from 'vuex-module-decorators';
import { PaginationParams } from '@/core';
import { PostFinderMixin, Post } from '@/post';
import CreatePostButtons from '@/post/ui/components/create-post-buttons.vue';
import PostSummaryList from '@/post/ui/components/post-summary-list.vue';

/**
 * Home page that shows off the newests new posts.
 */
@Component({
    components: {
        CreatePostButtons,
        Layout,
        PaginationNav,
        PostSummaryList
    }
})
export default class Home extends PostFinderMixin {
    public currentPage: number = 0;

    public async mounted() {
        await this.refreshPosts();
    }

    public async onPrevious() {
        this.currentPage--;
        this.refreshPosts();
    }

    public async onNext() {
        this.currentPage++;
        this.refreshPosts();
    }

    public async refreshPosts() {
        await this.$findPostsByNew(new PaginationParams(this.currentPage, Post.DEFAULT_PAGE_SIZE));
    }
}
</script>
