<script setup lang="ts">
import ProjectList from '~/domain/project/components/ProjectList.vue';

definePageMeta({
  name: 'Dashboard',
  icon: 'heroicons:document-duplicate-20-solid',
  middleware: 'authorization'
});

</script>

<template>
  <div>
    <h1 class="mb-2">Dashboard</h1>
    <ProjectList />
  </div>
</template>
