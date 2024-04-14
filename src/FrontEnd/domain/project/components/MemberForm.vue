<script setup lang="ts">
import { useMemberForm } from '../composable/useMemberForm';

const props = defineProps<{
  form: ReturnType<typeof useMemberForm>
}>();

const { schema, model: state, onSubmit, closeForm } = props.form;
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="onSubmit">
    <UFormGroup label="Username" name="username">
      <UInput v-model="state.username" placeholder="username.." />
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
