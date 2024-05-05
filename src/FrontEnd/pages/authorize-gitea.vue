<script lang="ts" setup>
definePageMeta({
  layout: false,
  middleware: 'authorize-gitea'
});
</script>

<template>
  <div class="w-full h-full flex justify-center items-center">
    <h1>If you see this page, something went wrong when authorizing</h1>
  </div>
</template>
