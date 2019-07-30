<template>
    <div id="app">
        <b-navbar variant="primary" type="dark">
            <b-navbar-brand href="#">
                <img src="@/assets/logo_white.png" class="nav-icon" />
            </b-navbar-brand>

            <div class="nav-buttons ml-auto">
                <b-button variant="outline-light">Log In</b-button>
                <b-button variant="outline-light">Sign Up</b-button>
            </div>
        </b-navbar>
        <router-view />
    </div>
</template>

<style>
.nav-icon {
    height: 64px;
}

.nav-buttons button {
    margin-left: 8px;
    margin-right: 8px;
}
#app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}
</style>
