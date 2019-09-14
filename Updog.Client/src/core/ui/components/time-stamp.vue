<template>
    <div class="d-inline-block" :title="date">
        <time>{{ humanReadableDifference }} ago</time>
        <span v-if="modified">*</span>
        <span>&nbsp;</span>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { DateUtils } from '@/core';

/**
 * Timestamp that displays a difference between two dates in a human
 * readable string.
 */
@Component({
    name: 'time-stamp'
})
export default class TimeStamp extends Vue {
    /**
     * The date the event occured at.
     */
    @Prop()
    public date!: Date;

    /**
     * If the resource has been modified since it's original creation.
     * Causes a "*" to appear.
     */
    @Prop({ default: false })
    public modified!: boolean;

    /**
     * How long ago the date occured from today.
     */
    public humanReadableDifference: string = '';

    public created() {
        this.humanReadableDifference = DateUtils.getDifferenceFromToday(this.date);
    }
}
</script>