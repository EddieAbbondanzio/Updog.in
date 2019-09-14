<template>
    <div v-if="user != null">
        <h4>{{ user.username }}</h4>
        <h5 class="text-muted">Member for&nbsp;{{ membershipStatus() }}</h5>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { User } from '@/user';
import { DateUtils } from '@/core';
/**
 * Component to show the details of a user on screen.
 */
@Component({
    name: 'user-summary'
})
export default class UserSummary extends Vue {
    /**
     * The user to display/
     */
    @Prop({ default: null })
    public user!: User | null;

    public membershipStatus() {
        return DateUtils.getDifferenceFromToday(this.user!.joinedDate);
    }
}
</script>