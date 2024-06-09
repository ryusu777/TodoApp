<script setup lang="ts">
import ProjectPhases from '~/domain/project/components/ProjectPhases.vue';
import ProjectRepositories from '~/domain/project/components/ProjectRepositories.vue';
import { useProject } from '~/domain/project/composable/useProject';
import ProjectHierarchies from '~/domain/project/components/ProjectHierarchies.vue';

definePageMeta({
  name: 'Detail',
  path: '/project/:id/detail',
  middleware: 'authorization'
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
    <ProjectHierarchies 
      :pending="state.isFetching"
      :refresh="onRefresh"
    />
  </div>
  <div>
    <ProjectRepositories 
      :project-id="state.project?.id || ''"
    />
  </div>
</template>
