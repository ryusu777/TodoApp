<script setup lang="ts">
import { useRepositories } from '~/domain/project/composable/useRepositories';
import ProjectRepository from './ProjectRepository.vue';
import ProjectRepositoryForm from './ProjectRepositoryForm.vue';

const props = defineProps<{
  projectId: string;
}>();
const repositories = useRepositories(props.projectId);
const editable = ref(false);

await useAsyncData(() => repositories.initialize());
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Attached Repositories</span>
      <div class="space-x-2">
        <UButton 
          icon="heroicons:arrow-path-rounded-square"
          size="xs"
          color="white"
          variant="ghost"
          @click="repositories.refresh"
          :loading="repositories.isRefreshing.value"
        />
        <UButton 
          icon="heroicons:plus"
          size="xs"
          color="green"
          variant="solid"
          @click="repositories.showForm"
        />
      </div>
    </div>
    <div class="flex flex-row flex-wrap gap-3 mt-3">
      <div v-for="repository of repositories.repositories.value" style="min-width: max-content; max-height: max-content;">
        <ProjectRepository :repository="repository" :editable="editable" />
      </div>
    </div>
  </div>

  <UModal 
    :model-value="repositories.show.value" 
    @update:model-value="repositories.closeForm()"
    prevent-close
  >
    <ProjectRepositoryForm :form="repositories" />
  </UModal>
</template>
