<template>
    <content-placeholders>
        <!-- Title -->
        <content-placeholders-heading class="comment-title-placeholder" />
        <content-placeholders-text :lines="1" class="comment-text-placeholder" />
    </content-placeholders>
</template>

<style lang="scss" scoped>
.comment-title-placeholder {
    width: 480px;
}

.comment-text-placeholder {
    width: 240px;
    margin-top: 8px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component({
    name: 'comment-loading-place-holder'
})
export default class CommentLoadingPlaceholder extends Vue {}
</script>