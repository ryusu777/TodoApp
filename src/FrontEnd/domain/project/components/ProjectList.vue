<script lang="ts" setup>
import { GetProjectDashboard, type ProjectItem } from '../api/projectApi';
import ProjectItemVue from './ProjectItem.vue';
import OnboardProjectForm from './OnboardProjectForm.vue';

const { data, refresh } = await useAsyncData(() => GetProjectDashboard());

// project onboarding form state
const showingCreateForm = ref(false);

function showCreateForm() {
  showingCreateForm.value = true;
}
</script>

<template>
  <div>
    <div class="mb-2 flex gap-x-4">
      <h1>Project List</h1>
      <UButton
        icon="heroicons:plus"
        size="md"
        @click="showCreateForm"
      />
    </div>
    <div class="flex flex-wrap gap-x-3">
      <ProjectItemVue
        v-for="project in data?.data?.projects"
        :key="project.code"
        :project="project"
        @refresh="refresh"
      />
    </div>
  </div>

  <UModal v-model="showingCreateForm">
    <OnboardProjectForm />
  </UModal>
</template>
