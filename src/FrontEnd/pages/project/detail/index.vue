<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';
import ProjectPhases from '~/domain/project/components/ProjectPhases.vue';
import ProjectMembers from '~/domain/project/components/ProjectMembers.vue';

definePageMeta({
  name: 'Detail',
  path: '/project/:id/detail'
});

const route = useRoute();
const projectCode = route.params.id.toString();
const { data: response, refresh, pending } = await GetProjectById(projectCode, true);
const projectDetail = computed(() => response.value?.data);
const projectName = computed(() => response?.value?.data?.name);

async function onRefresh() {
  await refresh();
}

const phases = computed(() => response.value?.data?.projectPhases || []);
const members = computed(() => response
  .value
  ?.data
  ?.projectMembers
  .map(e => { return { username: e } }) || []);

</script>

<template>
  <div>
    <h1 class="text-bold">{{ projectName }} Detail</h1>
    <p>{{ projectDetail?.description }}</p>
  </div>
  <div>
    <ProjectPhases 
      :phases="phases" 
      :project-id="projectDetail?.id || ''"
      :pending="pending"
      :refresh="onRefresh"
    />
  </div>
  <div>
    <ProjectMembers
      :members="members" 
      :project-id="projectDetail?.id || ''"
      :pending="pending"
      :refresh="onRefresh"
    />
  </div>
</template>
