<template>
    <div class="d-inline-block">
        <div class="d-flex flex-row grey--text text--darken-3">
            <span class="error--text">Are you sure?&nbsp;</span>
            <a class="grey--text text--darken-3" href="#" @click.prevent="$emit('yes')">yes</a>
            <span>&nbsp;/&nbsp;</span>
            <a class="grey--text text--darken-3" href="#" @click.prevent="$emit('no')">no</a>
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