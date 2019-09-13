<template>
    <div class="d-flex flex-column justify-content-center px-2">
        <div
            class="border rounded-circle d-flex flex-column justify-content-center bg-secondary"
            style="padding: 12px;"
        >
            <material-icon
                :icon="isTextPost ? 'message' : 'link'"
                variant="light"
                style="font-size: 32px!important;"
            />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../domain/post';
import MaterialIcon from '@/core/components/material-icon.vue';
import { PostType } from '@/post/domain/post-type';

/**
 * An icon for a text, or link post.
 */
@Component({
    name: 'post-icon',
    components: {
        MaterialIcon
    }
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