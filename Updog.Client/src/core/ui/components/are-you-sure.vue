<template>
    <div class="d-inline-block">
        <div class="d-flex flex-row">
            <span class="text-danger">Are you sure?&nbsp;</span>
            <b-button
                variant="link"
                class="m-0 p-0 d-flex"
                @click="$emit('yes')"
                style="border-width: 0px!important; height: 21px!important;"
            >
                <span class="text-sm text-muted">yes</span>
            </b-button>
            <span>&nbsp;/&nbsp;</span>
            <b-button
                variant="link"
                class="m-0 p-0 d-flex"
                @click="$emit('no')"
                style="border-width: 0px!important; height: 21px!important;"
            >
                <span class="text-sm text-muted">no</span>
            </b-button>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';

/**
 * Inline component to ask if a user is sure they want to delete something.
 */
@Component({
    name: 'are-you-sure'
})
export default class AreYouSure extends Vue {}
</script>