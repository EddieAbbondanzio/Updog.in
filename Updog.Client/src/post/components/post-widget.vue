<template>
    <div>
        <b-button variant="outline-primary" class="w-100 mt-3 mb-2">Submit a new link post</b-button>
        <b-button variant="outline-primary" class="w-100 mt-2 mb-3">Submit a new text post</b-button>
    </div>
</template>

<script lang="ts">
/**
 * Widget to create a new post.
 */
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component({
    name: 'post-widget'
})
export default class PostWidget extends Vue {}
</script>