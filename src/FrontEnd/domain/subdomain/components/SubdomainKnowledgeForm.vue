<script setup lang="ts">
import type { useSubdomainKnowledgeForm } from '../composable/useSubdomainKnowledgeForm';

const props = defineProps<{
  form: ReturnType<typeof useSubdomainKnowledgeForm>
}>();

const { schema, model: state, closeForm } = props.form;

const emit = defineEmits(['submit']);
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="emit('submit')">
    <UFormGroup label="Title" name="title">
      <UInput v-model="state.title" placeholder="title.." />
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
