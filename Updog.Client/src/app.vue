<template>
    <router-view />
</template>

<style>
@import './assets/css/style.css';
</style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { User } from './user/domain/user';

@Component({
    name: 'app'
})
export default class App extends Vue {}
</script>
