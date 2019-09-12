<template>
    <b-navbar variant="light" type="dark" class="border-bottom">
        <b-navbar-brand href="#" to="/">
            <img src="@/assets/logo.png" class="nav-icon" />
        </b-navbar-brand>

        <user-widget />
    </b-navbar>
</template>

<style>
.nav-icon {
    height: 64px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import UserWidget from '@/user/components/user-widget.vue';
import { User } from '@/user/domain/user';
import { UserLogin } from '@/user/domain/user-login';

/**
 * Nav bar atop the page. Shows the logged in user and brand image / name.
 */
@Component({
    name: 'nav-bar',
    components: {
        UserWidget
    }
})
export default class NavBar extends Vue {}
</script>