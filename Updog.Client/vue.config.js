module.exports = {
    css: {
        loaderOptions: {
            sass: {
                data: `@import "@/assets/styles/style.scss";`
            }
        }
    },
    devServer: {
        https: true
    }
};
