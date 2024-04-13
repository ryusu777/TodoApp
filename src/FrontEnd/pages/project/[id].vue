<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';

definePageMeta({
  name: 'Project Detail'
})

const route = useRoute();

const projectCode = route.params.id.toString();

const { data: response } = await GetProjectById(projectCode, true);

const projectName = computed(() => response?.value?.data?.name);

const breadcrumb = useBreadcrumb();

breadcrumb.setConverter('Project Detail', projectName);
</script>

<template>
  <h1>{{ projectName }}</h1>
</template>
