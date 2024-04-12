<script setup>
import Sidebar from '~/components/layouts/Sidebar.vue';
import Navbar from '~/components/layouts/Navbar.vue';
import { useBreadcrumb } from '~/composables/useBreadcrumb';

const breadcrumb = useBreadcrumb(useRouter());
</script>

<template>
  <div class="flex flex-col h-full">
    <Navbar />
    <UDivider class="px-5" />

    <div class="flex flex-row h-full w-full">
      <Sidebar />
      <div class="p-5 h-full w-full">
        <UBreadcrumb 
          :links="breadcrumb.links.value"
        />
        <slot />
      </div>
    </div>
  </div>
</template>

<style>
html,body, #__nuxt, #__layout{
  height: 100%!important;
  width: 100%!important;
}
</style>
