<template>
    <div>
        <space-quick-access-bar />
        <b-navbar variant="light" type="dark" class="border-bottom d-flex flex-row">
            <b-navbar-brand href="#" to="/">
                <img src="@/assets/logo.png" class="nav-icon" />
            </b-navbar-brand>

            <div class="flex-grow-1 d-flex flex-row justify-content-center" v-if="spaceName != ''">
                <h1 class="mb-0 pb-0">
                    <router-link
                        :to="{name: 'space', params: { spaceName: spaceName}}"
                        class="text-dark"
                    >{{spaceName}}</router-link>
                </h1>
            </div>

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
export default class NavBar extends Vue {
    get spaceName() {
        if (this.$route.params.spaceName != null) {
            return this.$route.params.spaceName;
        } else {
            return '';
        }
    }
}
</script>