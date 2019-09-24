
<template>
    <v-card class="mb-3 post-placeholder">
        <content-placeholders class="d-flex flex-row pr-4 py-4 pl-2" style="height:112px;">
            <!-- Icon -->
            <content-placeholders-img class="icon-placeholder align-self-center" />

            <!-- Title -->
            <div class="align-self-start">
                <content-placeholders-heading class="title-placeholder" />
                <content-placeholders-text :lines="1" class="mt-3 actions-placeholder" />
            </div>
        </content-placeholders>
    </v-card>
</template>

<style lang="scss" scoped>
.post-placeholder {
    height: 112px;
}

.icon-placeholder {
    margin-left: 44px;
    margin-right: 8px;
    height: 48px;
    width: 48px;
    border-radius: 50%;
}

.title-placeholder {
    width: 480px;
}

.actions-placeholder {
    height: 12px !important;
    width: 240px;
    overflow: hidden !important;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component({
    name: 'post-loading-place-holder'
})
export default class PostLoadingPlaceHolder extends Vue {}
</script>