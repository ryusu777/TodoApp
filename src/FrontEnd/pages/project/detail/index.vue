<script setup lang="ts">
import { GetProjectById } from '~/domain/project/api/projectApi';
import ProjectPhases from '~/domain/project/components/ProjectPhases.vue';
import ProjectMembers from '~/domain/project/components/ProjectMembers.vue';
import { useProject } from '~/domain/project/composable/useProject';

definePageMeta({
  name: 'Detail',
  path: '/project/:id/detail'
});

const route = useRoute();
const projectCode = route.params.id.toString();
const state = useProject();

await state.fetch(projectCode, true);

const projectName = computed(() => state.project?.name);

async function onRefresh() {
  await state.fetch(projectCode, false);
}

const phases = computed(() => state.project?.projectPhases || []);
const members = computed(() => state
  .project
  ?.projectMembers
  .map(e => { return { username: e } }) || []);

</script>

<template>
  <div>
    <h1 class="text-bold">{{ projectName }} Detail</h1>
    <p>{{ state.project?.description }}</p>
  </div>
  <div>
    <ProjectPhases 
      :phases="phases" 
      :project-id="state.project?.id || ''"
      :pending="state.isFetching"
      :refresh="onRefresh"
    />
  </div>
  <div>
    <ProjectMembers
      :members="members" 
      :project-id="state.project?.id || ''"
      :pending="state.isFetching"
      :refresh="onRefresh"
    />
  </div>
</template>
