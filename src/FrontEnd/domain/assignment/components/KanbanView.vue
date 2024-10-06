<script setup lang="ts">
import type { useAssignmentForm } from '../composables/useAssignmentForm';
import { useAssignmentState } from '../composables/useAssignmentState';

const props = defineProps<{
  state: ReturnType<typeof useAssignmentState>;
  form: ReturnType<typeof useAssignmentForm>;
}>();

const filteredAssignments = computed(() => {
  return Object.groupBy(props.state.assignments.value, ({ status }) => status);
});

const openAssignments = computed(() => (filteredAssignments.value.New || []).concat(filteredAssignments.value.Revised || []));
</script>

<template>
  <KanbanBoard
    :state="state"
    :form="form"
    type="New"
    :assignments="openAssignments"
  />
  <KanbanBoard
    :state="state"
    :form="form"
    type="OnProgress"
    :assignments="filteredAssignments.OnProgress || []"
  />
  <KanbanBoard
    :state="state"
    :form="form"
    type="WaitingReview"
    :assignments="filteredAssignments.WaitingReview || []"
  />
  <KanbanBoard
    :state="state"
    :form="form"
    type="Completed"
    :assignments="filteredAssignments.Completed || []"
  />
</template>
