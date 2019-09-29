<template>
    <layout noSideBar="true">
        <v-row align="center" justify="center">
            <v-col cols="12" sm="8" md="4" v-if="!clickedLogin">
                <v-card>
                    <v-form class="bg-light border px-5 pt-5 pb-3 my-5">
                        <div class="pb-3">
                            <h1 class="pb-0 mb-0">Admin Panel</h1>
                        </div>

                        <v-text-field placeholder="Username" />
                        <v-text-field type="password" placeholder="Password" />

                        <v-btn class="mr-2" color="primary" @click="clickedLogin = true">Login</v-btn>
                    </v-form>
                </v-card>
            </v-col>
            <v-col cols="8" offset="2" v-else>
                <iframe
                    width="1080"
                    height="720"
                    src="https://www.youtube.com/embed/dQw4w9WgXcQ?autoplay=1"
                    frameborder="0"
                    allowfullscreen
                ></iframe>
            </v-col>
        </v-row>
    </layout>
</template>

<script lang="ts">
import Layout from '@/core/ui/components/layout.vue';
import { Component, Vue, Prop } from 'vue-property-decorator';
@Component({
    name: 'admin',
    components: {
        Layout
    }
})
export default class Admin extends Vue {
    public clickedLogin = false;
}
</script>