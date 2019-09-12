<template>
    <router-view />
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { User } from './user/domain/user';

@Component({
    name: 'app'
})
export default class App extends Vue {}
</script>
