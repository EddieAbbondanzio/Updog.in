<template>
    <div>
        <v-btn
            class="mb-3"
            color="primary"
            block
            outlined
            @click="onClick(false)"
        >Submit a new link post</v-btn>
        <v-btn color="primary" block outlined @click="onClick(true)">Submit a new text post</v-btn>
    </div>
</template>

<script lang="ts">
/**
 * Widget to create a new post.
 */
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component({
    name: 'create-post-buttons'
})
export default class CreatePostButtons extends Vue {
    public onClick(isText: boolean) {
        this.$router.push({ path: '/submit', query: { isText: isText ? 'true' : 'false' } });
    }
}
</script>