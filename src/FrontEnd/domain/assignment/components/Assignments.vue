<script setup lang="ts">
import { createReusableTemplate, type DefineTemplateComponent } from '@vueuse/core';
import { useAssignmentForm } from '../composables/useAssignmentForm';
import { useAssignmentState } from '../composables/useAssignmentState';
import KanbanBoard from './KanbanBoard.vue';
import AssignmentForm from './AssignmentForm.vue';

const props = defineProps<{
  projectId: string;
  subdomainId: string;
}>();

const toast = useToast();

const state = useAssignmentState(props.projectId);

const form = useAssignmentForm(props.projectId);

const initial = ref(true);

const [DefineTemplate, ReuseTemplate] = createReusableTemplate();

defineExpose({
  ReuseTemplate: ReuseTemplate
});

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

async function submit() {
  const error = await form.submit();
  if (error)
    toast.add({
      title: 'Error',
      description: error,
      color: 'red'
    })
  else {
    toast.add({
      title: 'Success',
      description: 'Successfully added assignment'
    });
    form.closeForm();
    await state.fetch(false);
  }
}
</script>

<template>
  <DefineTemplate>
    <div>
      <UButton 
        icon="heroicons:plus"
        label="New Assignment"
        size="md"
        @click="form.create(subdomainId)"
      />
    </div>
  </DefineTemplate>
  <div class="flex gap-x-3">
    <KanbanBoard
      :state="state"
      :form="form"
      type="Todo"
      :assignments="filteredAssignments.New || []"
    />
    <KanbanBoard
      :state="state"
      :form="form"
      type="In Progress"
      :assignments="filteredAssignments.OnProgress || []"
    />
    <KanbanBoard
      :state="state"
      :form="form"
      type="Waiting Review"
      :assignments="filteredAssignments.WaitingReview || []"
    />
    <KanbanBoard
      :state="state"
      :form="form"
      type="Done"
      :assignments="filteredAssignments.Completed || []"
    />
  </div>
  <UModal
    :model-value="form.show.value"
    @update:model-value="form.closeForm"
  >
    <AssignmentForm 
      :form="form" 
      @submit="submit"
    />
  </UModal>
</template>
