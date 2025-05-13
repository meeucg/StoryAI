const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      '/api': {
        target: 'https://localhost:44359',
        changeOrigin: true,
        pathRewrite: {
          '^/api': ''
        },
        secure: false
      }
    }
  }
})
