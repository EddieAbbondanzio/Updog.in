<template>
    <v-avatar color="grey darken-2" size="48">
        <v-icon dark medium>{{ isTextPost ? 'message' : 'link'}}</v-icon>
    </v-avatar>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostType, Post } from '@/post';

/**
 * An icon for a text, or link post.
 */
@Component({
    name: 'post-icon'
})
export default class PostIcon extends Vue {
    /**
     * The post to show in the icon.
     */
    @Prop()
    public post!: Post;

    /**
     * Check to see if the post is a text based post.
     */
    get isTextPost() {
        return this.post.type === PostType.Text;
    }
}
</script>