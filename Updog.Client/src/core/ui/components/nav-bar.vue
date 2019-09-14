<template>
    <div>
        <space-quick-access-bar />
        <b-navbar variant="light" type="dark" class="border-bottom">
            <b-navbar-brand href="#" to="/">
                <img src="@/assets/logo.png" class="nav-icon" />
            </b-navbar-brand>

            <user-widget />
        </b-navbar>
    </div>
</template>

<style>
.nav-icon {
    height: 64px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import UserWidget from '@/user/ui/components/user-widget.vue';
import { User } from '@/user';
import { UserLogin } from '@/user';
import SpaceQuickAccessBar from '@/space/ui/components/space-quick-access-bar.vue';

/**
 * Nav bar atop the page. Shows the logged in user and brand image / name.
 */
@Component({
    name: 'nav-bar',
    components: {
        UserWidget,
        SpaceQuickAccessBar
    }
})
export default class NavBar extends Vue {}
</script>