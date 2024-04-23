<script setup lang="ts">
import { useAssignmentState } from '../composables/useAssignmentState';
import KanbanBoard from './KanbanBoard.vue';

const props = defineProps<{
  projectId: string
}>();

const state = useAssignmentState(props.projectId);

const initial = ref(true);

await state.fetch(true);
initial.value = false;

onMounted(async () => {
  if (!initial.value) {
    await state.fetch(false);
  }
});

const filteredAssignments = computed(() => {
  return Object.groupBy(state.assignments.value, ({ status }) => status);
});

</script>
<template>
  <KanbanBoard
    type="Todo"
    :assignments="filteredAssignments.New || []"
  />
</template>
