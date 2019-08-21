<template>
    <time>{{ humanReadableDifference }}</time>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { DateUtils } from '../core/utils/date-utils';

/**
 * Timestamp that displays a difference between two dates in a human
 * readable string.
 */
@Component({
    name: 'time-stamp'
})
export default class DateTimeStamp extends Vue {
    /**
     * The date the event occured at.
     */
    @Prop()
    public date!: Date;

    /**
     * How long ago the date occured from today.
     */
    public humanReadableDifference: string = '';

    public created() {
        this.humanReadableDifference = DateUtils.getDifferenceFromToday(this.date);
    }
}
</script>