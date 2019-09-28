<template>
    <v-card class="d-flex flex-row pa-3" v-if="pagination.pageCount() > 1">
        <v-btn
            color="primary"
            outlined
            class="d-flex flex-row align-self-center mr-1"
            v-if="pagination.hasPreviousPage()"
            @click="$emit('previous')"
        >
            <v-icon color="primary">navigate_before</v-icon>Previous
        </v-btn>
        <v-btn
            color="primary"
            outlined
            class="d-flex flex-row align-self-center ml-1"
            v-if="pagination.hasNextPage()"
            @click="$emit('next')"
        >
            Next
            <v-icon color="primary">navigate_next</v-icon>
        </v-btn>
    </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PaginationInfo } from '@/core/pagination/pagination-info';

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