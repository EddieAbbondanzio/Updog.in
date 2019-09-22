<template>
    <v-app-bar app color="grey lighten-4">
        <router-link :to="{name: 'home'}">
            <img src="@/assets/logo.png" class="nav-icon" />
        </router-link>

        <div class="flex-grow-1 d-flex flex-row justify-content-center" v-if="spaceName != ''">
            <h1 class="mb-0 pb-0">
                <router-link
                    :to="{name: 'space', params: { spaceName: spaceName}}"
                    class="text-dark"
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