<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';
import ProjectPhases from '~/domain/project/components/ProjectPhases.vue';

definePageMeta({
  name: 'Detail',
  path: '/project/:id/detail'
});

const route = useRoute();
const projectCode = route.params.id.toString();
const { data: response } = await GetProjectById(projectCode, true);
const projectDetail = response.value?.data;
const projectName = computed(() => response?.value?.data?.name);

</script>

<template>
  <div>
    <h1 class="text-bold">{{ projectName }} Detail</h1>
    <p>{{ projectDetail?.description }}</p>
  </div>
  <div>
    <ProjectPhases :phases="projectDetail?.projectPhases || []" />
  </div>
</template>
