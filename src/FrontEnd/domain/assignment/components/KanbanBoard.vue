<script setup lang="ts">
import type { Assignment } from '../api/assignmentApi';
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import type { useAssignmentState } from '../composables/useAssignmentState';
import AssignmentVue from './Assignment.vue';

defineProps<{
  assignments: Assignment[];
  type: string;
  form: ReturnType<typeof useAssignmentForm>;
  state: ReturnType<typeof useAssignmentState>;
}>();

</script>
<template>
  <div class="w-[250px] flex flex-col gap-y-2">
    <UCard
      :ui="{
        body: {
          padding: 'px-3 py-2 sm:px-3 sm:py-2'
        }
      }"
    >
      <div class="flex justify-between items-center">
        <p>{{ type }}</p>
      </div>
    </UCard>
    <AssignmentVue 
      :form="form"
      :state="state"
      :assignment="assignment" 
      v-for="assignment in assignments"
    />
  </div>
</template>
