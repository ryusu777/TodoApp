// https://nuxt.com/docs/api/configuration/nuxt-config
//
export default defineNuxtConfig({
  devtools: { enabled: true },
  typescript: {
    typeCheck: true
  },
  routeRules: {
    '/api/**': {
      proxy: { to: process.env.API_URL + '/api/**' }
    }
  },
  modules: ['@nuxt/ui', '@pinia/nuxt'],
  css: ['~/public/css/main.css'],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {}
    }
  },
  ui: {
    icons: {}
  },
});
