<template>
    <span>
        <router-link :to="`/user/${user.username}`">{{ user.username }}</router-link>&nbsp;
    </span>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { User } from '../domain/user';

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
}
</script>