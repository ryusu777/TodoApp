// https://nuxt.com/docs/api/configuration/nuxt-config
//
export default defineNuxtConfig({
  devtools: { enabled: true },
  typescript: {
    typeCheck: true
  },
  modules: ['@nuxt/ui', '@pinia/nuxt', '@nuxt/test-utils/module'],
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
  runtimeConfig: {
    public: {
      API_URL: process.env.API_URL
    }
  }
});
