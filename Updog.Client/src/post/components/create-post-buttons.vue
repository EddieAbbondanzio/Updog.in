<template>
    <div>
        <b-button
            variant="outline-primary"
            class="w-100 mb-2"
            @click="onClick(false)"
        >Submit a new link post</b-button>
        <b-button
            variant="outline-primary"
            class="w-100 mt-2 mb-3"
            @click="onClick(true)"
        >Submit a new text post</b-button>
    </div>
</template>

<script lang="ts">
/**
 * Widget to create a new post.
 */
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Context } from '../../core/context';
@Component({
    name: 'post-widget'
})
export default class CreatePostButtons extends Vue {
    public onClick(isText: boolean) {
        this.$router.push({ path: '/submit', query: { isText: isText ? 'true' : 'false' } });
    }
}
</script>