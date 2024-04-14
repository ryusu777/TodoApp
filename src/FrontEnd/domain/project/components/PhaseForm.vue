<script setup lang="ts">
import { usePhaseForm } from '../composable/usePhaseForm';
import InputDate from '~/forms/components/InputDate.vue';

const props = defineProps<{
  form: ReturnType<typeof usePhaseForm>
}>();

const { schema, model: state, onSubmit, closeForm } = props.form;
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="onSubmit">
    <UFormGroup label="Phase name" name="name">
      <UInput v-model="state.name" placeholder="development.." />
    </UFormGroup>

    <UFormGroup label="Description" name="description" class="mt-3">
      <UInput v-model="state.description" placeholder="describe the phase.." />
    </UFormGroup>

    <div class="flex flex-row gap-3 mt-3">
      <UFormGroup label="Start Date" name="startDate">
        <InputDate v-model="state.startDate" />
      </UFormGroup>

      <UFormGroup label="End Date" name="endDate">
        <InputDate v-model="state.endDate" />
      </UFormGroup>
    </div>

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
