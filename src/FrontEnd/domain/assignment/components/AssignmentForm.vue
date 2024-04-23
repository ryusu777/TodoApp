<script setup lang="ts">
import type { UTextarea } from '#build/components';
import { useAssignmentForm } from '../composables/useAssignmentForm';
import InputDate from '~/forms/components/InputDate.vue';

const props = defineProps<{
  form: ReturnType<typeof useAssignmentForm>
}>();

const { schema, model: state, submit, closeForm } = props.form;

const emit = defineEmits(['submit']);
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="emit('submit')">
    <UFormGroup label="Title" name="title">
      <UInput v-model="state.title" placeholder="title.." />
    </UFormGroup>

    <UFormGroup label="Description" name="description">
      <UInput v-model="state.description" placeholder="this subdomain is for.." />
    </UFormGroup>

    <UFormGroup label="Deadline" name="deadline">
      <InputDate v-model="state.deadline" />
    </UFormGroup>

    <UFormGroup label="Assignees" name="assignees">
      <USelectMenu 
        v-model="state.assignees"
        :options="['joseryu', 'nicoabel', 'agungsukmawan']"
        multiple
      />
    </UFormGroup>

    <UFormGroup label="Phase" name="phaseId">
      <USelectMenu 
        v-model="state.phaseId"
        :options="['UR', 'DR', 'PR']"
      />
    </UFormGroup>

    <UFormGroup label="Subdomain" name="subdomainId">
      <USelectMenu 
        v-model="state.subdomainId"
        :options="['Assignment', 'Project', 'Subdomain']"
      />
    </UFormGroup>

    <UFormGroup label="Subdomain" name="subdomainId">
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
