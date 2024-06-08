<script setup lang="ts">
import { object, string } from 'yup';
import SelectMember from './SelectMember.vue';

const props = defineProps<{
  projectId: string;
}>();

const emit = defineEmits(['close', 'submit']);
const schema = object({
  username: string().required('Username is required'),
});

const state = reactive({
  username: '',
});

function onSubmit() {
  emit('submit', state.username);
}
</script>

<template>
  <UForm :schema="schema" :state="state" class="p-5 flex flex-col gap-5" @submit="onSubmit">
    <UFormGroup label="Username" name="username">
      <SelectMember
        :projectId="props.projectId"
        v-model="state.username"
      />
    </UFormGroup>

    <div class="flex justify-end gap-3">
      <UButton 
        @click="emit('close')" 
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
