<template>
    <div class="d-flex flex-row">
        <b-button
            variant="outline-dark"
            class="d-flex flex-row align-self-center mr-1"
            v-if="pagination.hasPreviousPage()"
            @click="$emit('previous')"
        >
            Previous
            <material-icon icon="navigate_before" variant="dark" />
        </b-button>
        <b-button
            variant="outline-dark"
            class="d-flex flex-row align-self-center ml-1"
            v-if="pagination.hasNextPage()"
            @click="$emit('next')"
        >
            Next
            <material-icon icon="navigate_next" variant="dark" />
        </b-button>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PaginationInfo } from '../pagination/pagination-info';
import MaterialIcon from '@/core/components/material-icon.vue';
@Component({
    name: 'pagination-navigation',
    components: {
        MaterialIcon
    }
})
export default class PaginationNavigation extends Vue {
    /**
     * The pagination info to work with.
     */
    @Prop()
    public pagination!: PaginationInfo;
}
</script>