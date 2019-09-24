<template>
    <v-app-bar app color="accent" dark height="72">
        <v-app-bar-nav-icon @click="$emit('toggleNav')" />

        <router-link :to="{name: 'home'}" class="d-flex align-center">
            <img src="@/assets/logo_white.png" class="nav-icon" />
        </router-link>

        <div class="flex-grow-1 d-flex flex-row justify-center" v-if="spaceName != ''">
            <h1 class="mb-0 pb-0">
                <router-link
                    :to="{name: 'space', params: { spaceName: spaceName}}"
                    class="white--text text-decoration-none"
                >{{spaceName}}</router-link>
            </h1>
        </div>

        <user-widget />
    </v-app-bar>
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

/**
 * Nav bar atop the page. Shows the logged in user and brand image / name.
 */
@Component({
    name: 'app-bar',
    components: {
        UserWidget
    }
})
export default class AppBar extends Vue {
    get spaceName() {
        if (this.$route.params.spaceName != null) {
            return this.$route.params.spaceName;
        } else {
            return '';
        }
    }
}
</script>