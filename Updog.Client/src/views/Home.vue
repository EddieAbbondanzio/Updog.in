<template>
    <div class="home">
        <img alt="Vue logo" src="../assets/logo.png" />
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src
import axios from 'axios';

@Component({
    components: {
        HelloWorld
    }
})
export default class Home extends Vue {
    public async created() {
        const r = await axios.get('cheese');
    }
}
</script>
