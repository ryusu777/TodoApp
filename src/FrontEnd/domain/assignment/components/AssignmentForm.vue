<script setup lang="ts">
import type { UTextarea } from '#build/components';
import { useProject } from '~/domain/project/composable/useProject';
import { useAssignmentForm } from '../composables/useAssignmentForm';
import InputDate from '~/forms/components/InputDate.vue';
import { useSubdomainTabs } from '~/domain/subdomain/composable/useSubdomainTabs';
import SelectProjectRepository from '~/domain/gitea-integration/components/SelectProjectRepository.vue';
import type { GiteaRepository } from '~/domain/gitea-integration/api/giteaIntegrationApi';

const props = defineProps<{
  form: ReturnType<typeof useAssignmentForm>
}>();

const { schema, model: state, submit: formSubmit, closeForm, show } = props.form;

const emit = defineEmits(['submit']);

const project = useProject();

const subdomain = useSubdomainTabs();

const subdomainOptions = computed(() => subdomain.tabs.map(e => {
  return {
    id: e.subdomain.id,
    title: e.subdomain.title
  }
}));

const selectedRepository = ref<GiteaRepository>();

function setSelectedRepository(value: GiteaRepository) {
  selectedRepository.value = value;
  state.giteaRepositoryId = value.id;
}

const selectedSubdomain = ref(subdomainOptions.value.find(e => e.id === state.subdomainId));

const phaseOptions = computed(() => project.phases?.map(e => {
  return {
    id: e.id,
    name: e.name
  }
}));

const selectedPhase = ref(project.phases?.find(e => e.id === state.phaseId));

function submit() {
  state.subdomainId = selectedSubdomain.value?.id;
  state.phaseId = selectedPhase.value?.id;
  emit('submit');
}

</script>

<template>
  <UForm 
    :schema="schema" 
    :state="state" 
    class="p-5 flex flex-col gap-5" 
    @submit="submit"
  >
    <div class="flex gap-x-2">
      <UFormGroup label="Title" name="title" class="w-full">
        <UInput v-model="state.title" placeholder="title.." />
      </UFormGroup>
    </div>
    <div class="flex gap-x-2">
      <UFormGroup label="Repository" name="giteaRepositoryId" v-if="!state.id">
        <SelectProjectRepository 
          :project-id="form.projectId"
          :selected="selectedRepository"
          @update:selected="setSelectedRepository"
        />
      </UFormGroup>

      <UFormGroup label="Deadline" name="deadline">
        <InputDate v-model="state.deadline" />
      </UFormGroup>

      <UFormGroup label="Phase" name="phaseId" class="flex-grow">
        <USelectMenu
          option-attribute="name"
          v-model="selectedPhase"
          :options="phaseOptions"
        />
      </UFormGroup>
    </div>

    <div class="flex gap-x-2">
      <UFormGroup label="Assignees" name="assignees" class="flex-1">
        <USelectMenu 
          v-model="state.assignees"
          :options="project.members"
          multiple
        />
      </UFormGroup>

      <UFormGroup label="Reviewer" name="reviewer" class="flex-1">
        <USelectMenu 
          v-model="state.reviewer"
          :options="project.members"
        />
      </UFormGroup>

      <UFormGroup label="Subdomain" name="subdomainId" class="flex-1">
        <USelectMenu
          option-attribute="title"
          v-model="selectedSubdomain"
          :options="subdomainOptions"
        />
      </UFormGroup>
    </div>

    <UFormGroup label="Description" name="description">
      <UTextarea 
        v-model="state.description"
      />
    </UFormGroup>

    <div class="flex justify-end gap-3">
      <UButton 
        @click="closeForm" 
        label="Cancel" 
        icon="heroicons:x-circle-16-solid" 
        color = "red" 
      />
      <UButton 
        type="submit" 
        label="Save" 
        icon="heroicons:paper-airplane-16-solid" 
      />
    </div>
  </UForm>
</template>
