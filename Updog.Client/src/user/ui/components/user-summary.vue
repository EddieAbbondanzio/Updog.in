<template>
    <v-card v-if="user != null" class="pa-3">
        <v-card-title>{{ user.username }}</v-card-title>

        <v-card-text>
            <h5 class="subtitle-1 text-lighten-2">
                Member for
                <span class="text-dark">{{ membershipStatus() }}</span>
            </h5>

            <div>
                <span>Post Karma:&nbsp;</span>
                <span class="black--text">{{ user.postKarma}}</span>
            </div>
            <div>
                <span>Comment Karma:&nbsp;</span>
                <span class="black--text">{{ user.commentKarma}}</span>
            </div>
        </v-card-text>
    </v-card>
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