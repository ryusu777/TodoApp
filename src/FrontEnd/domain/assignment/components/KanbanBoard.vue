<script setup lang="ts">
import type { AssignmentStatusEnum } from '../api/assignmentApi';
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import type { NumberedAssignment, useAssignmentState } from '../composables/useAssignmentState';
import AssignmentVue from './Assignment.vue';

const props = defineProps<{
  assignments: NumberedAssignment[];
  type: AssignmentStatusEnum;
  form: ReturnType<typeof useAssignmentForm>;
  state: ReturnType<typeof useAssignmentState>;
}>();

const label = computed(() => {
  switch(props.type) {
    case 'New':
      return 'Open';
    case 'Completed':
      return 'Completed';
    case 'WaitingReview':
      return 'Waiting Review';
    case 'OnProgress':
      return 'In Progress';
  }
});

const bgColor = computed(() => {
  switch(props.type) {
    case 'New':
      return 'bg-red-400 dark:bg-red-400';
    case 'Completed':
      return 'bg-primary dark:bg-primary';
    case 'WaitingReview':
      return 'bg-yellow-400 dark:bg-yellow-400';
    case 'OnProgress':
      return 'bg-blue-400 dark:bg-blue-400';
  }
});

</script>
<template>
  <div class="w-[250px] flex flex-col gap-y-2">
    <UCard
      :ui="{
        background: bgColor,
        body: {
          padding: 'px-3 py-2 sm:px-3 sm:py-2',
        }
      }"
    >
      <div class="flex justify-between items-center text-black">
        <p>{{ label }}</p>
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
