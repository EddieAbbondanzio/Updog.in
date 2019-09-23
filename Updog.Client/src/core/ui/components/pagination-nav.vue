<template>
    <div class="d-flex flex-row">
        <v-btn
            outlined
            class="d-flex flex-row align-self-center mr-1"
            v-if="pagination.hasPreviousPage()"
            @click="$emit('previous')"
        >
            Previous
            <v-icon color="grey darken-3">navigate_before</v-icon>
        </v-btn>
        <v-btn
            outlined
            class="d-flex flex-row align-self-center ml-1"
            v-if="pagination.hasNextPage()"
            @click="$emit('next')"
        >
            Next
            <v-icon color="grey darken-3">navigate_next</v-icon>
        </v-btn>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PaginationInfo } from '@/core';
@Component({
    name: 'pagination-nav'
})
export default class PaginationNav extends Vue {
    /**
     * The pagination info to work with.
     */
    @Prop()
    public pagination!: PaginationInfo;
}
</script>