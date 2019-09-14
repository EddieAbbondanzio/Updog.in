<template>
    <span>
        <router-link :to="{ name: 'user', params: { username: user.username}}">{{ formattedName }}</router-link>&nbsp;
    </span>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { User } from '@/user';

/**
 * Link to a user's profile page.
 */
@Component({
    name: 'user-link'
})
export default class UserLink extends Vue {
    /**
     * The user it links to.
     */
    @Prop()
    public user!: User;

    /**
     * If the user name should be prefixed with: u/
     */
    @Prop({ default: false })
    public showPrefix!: boolean;

    /**
     * Get the formatted name of the user to display.
     */
    get formattedName() {
        return this.showPrefix ? `u/${this.user.username}` : this.user.username;
    }
}
</script>