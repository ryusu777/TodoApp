<script setup lang="ts">
import { useHierarchyForm } from '~/domain/project/composable/useHierarchyForm';
import type { Hierarchy } from '../api/projectApi';

const emit = defineEmits(['refresh']);
const props = defineProps<{
  form: ReturnType<typeof useHierarchyForm>;
  hierarchies: Hierarchy[];
}>();

const toast = useToast();

const selectableHierarchy = computed(() => props.hierarchies.filter(h => h.id !== props.form.model.id));

const { schema, model: state, onSubmit, closeForm } = props.form;

async function submit() {
  const errorDescription = await onSubmit();
  if (errorDescription) {
    toast.add({
      title: 'Error',
      description: errorDescription,
      color: 'red'
    });
    return;
  }
  emit('refresh');
  closeForm();
}
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="submit">
    <UFormGroup label="Role (Hierarchy) name" name="name">
      <UInput v-model="state.name" placeholder="programmer.." />
    </UFormGroup>

    <UFormGroup label="Superior" name="superiorHierarchyId" class="flex-1">
      <USelectMenu
        option-attribute="name"
        value-attribute="id"
        v-model="state.superiorHierarchyId"
        :options="selectableHierarchy"
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
