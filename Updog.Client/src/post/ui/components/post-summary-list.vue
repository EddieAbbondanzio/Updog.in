<template>
    <div v-if="posts != null">
        <div v-if="posts.length > 0">
            <post-summary v-for="post in posts" :post="post" v-bind:key="post.id" />
        </div>
        <v-card v-else class="pa-3 mb-3 error--text">There's nothing here!</v-card>
    </div>
    <div v-else>
        <post-loading-place-holder v-for="i in 4" :key="i" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../domain/post';
import PostSummary from '@/post/ui/components/post-summary.vue';
import PostLoadingPlaceHolder from '@/post/ui/components/post-loading-place-holder.vue';

@Component({
    name: 'post-summary-list',
    components: {
        PostSummary,
        PostLoadingPlaceHolder
    }
})
export default class PostSummaryList extends Vue {
    @Prop()
    public posts!: Post[] | null;
}
</script>