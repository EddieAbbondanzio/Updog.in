<template>
    <div id="app">
        <b-navbar variant="light" type="dark" class="border-bottom">
            <b-navbar-brand href="#">
                <img src="@/assets/logo.png" class="nav-icon" />
            </b-navbar-brand>

            <div class="nav-buttons ml-auto">
                <b-button variant="outline-dark" to="login">Log In</b-button>
                <b-button variant="outline-dark" to="signup">Sign Up</b-button>
            </div>
        </b-navbar>
        <router-view />
    </div>
</template>

<style>
.nav-icon {
    height: 64px;
}

.nav-buttons a {
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
