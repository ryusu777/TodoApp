<script setup lang="ts">
import ProjectList from '~/domain/project/components/ProjectList.vue';

definePageMeta({
  name: 'Dashboard',
  icon: 'heroicons:document-duplicate-20-solid',
  middleware: 'authorization'
});

</script>

<template>
  <ProjectList />
</template>
