<template>
    <div class="w-100">
        <v-btn
            class="mb-3 px-0 mx-0"
            color="primary"
            block
            outlined
            @click="onClick(false)"
        >Submit a new link post</v-btn>
        <v-btn
            class="pa-0 ma-0"
            color="primary"
            block
            outlined
            @click="onClick(true)"
        >Submit a new text post</v-btn>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

/**
 * Widget to create a new post.
 */
@Component({
    name: 'create-post-buttons'
})
export default class CreatePostButtons extends Vue {
    public onClick(isText: boolean) {
        if (this.$route.params.spaceName != null) {
            this.$router.push({ name: 'spaceSubmit', query: { isText: isText ? 'true' : 'false' } });
        } else {
            this.$router.push({ name: 'submit', query: { isText: isText ? 'true' : 'false' } });
        }
    }
}
</script>