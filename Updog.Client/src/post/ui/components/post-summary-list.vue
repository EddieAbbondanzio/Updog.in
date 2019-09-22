<template>
    <div>
        <post-summary v-for="post in posts" :post="post" v-bind:key="post.id" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../domain/post';
import PostSummary from '@/post/ui/components/post-summary.vue';

@Component({
    name: 'post-summary-list',
    components: {
        PostSummary
    }
})
export default class PostSummaryList extends Vue {
    @Prop()
    public posts!: Post[];
}
</script>