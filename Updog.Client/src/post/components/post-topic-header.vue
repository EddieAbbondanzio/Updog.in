<template>
    <div class="bg-light border" v-if="post != null">
        <h1 class="mb-0">{{ post.title }}</h1>
        <p class="text-muted" :title="post.date">Posted {{ readableDate}} ago by {{ post.author }}</p>
        <p>{{ post.body }}</p>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostInfo } from '../common/post-info';
import { DateUtils } from '@/core/utils/date-utils';

@Component({
    name: 'post-topic-header'
})
export default class PostTopicHeader extends Vue {
    /**
     * The post to show.
     */
    @Prop()
    public post!: PostInfo;

    /**
     * Fancy message for how long ago the post was made.
     */
    public readableDate: string = '';

    public created() {
        this.readableDate = DateUtils.getDifferenceFromToday(this.post.date);
    }
}
</script>