<script lang="ts" setup>
import type { NumberedAssignment } from '../composables/useAssignmentState';
import { UpdateAssignment } from '../api/assignmentApi';
import IssueNumber from './IssueNumber.vue';
import type { useSubdomainTabs } from '~/domain/subdomain/composable/useSubdomainTabs';

const props = defineProps<{
  assignment: NumberedAssignment;
  projectId: string;
  subdomainTabs: ReturnType<typeof useSubdomainTabs>;
}>();

const emit = defineEmits(['refresh']);

const subdomainOptions = computed(() => props
  .subdomainTabs
  .tabs
  .map(e => e.subdomain)
);

const selected = ref<typeof subdomainOptions.value[number]>();

const apiUtils = useApiUtils();

const toast = useToast();

const isSubmitting = ref(false);

async function submit() {
  isSubmitting.value = true;
  await apiUtils.try(() => UpdateAssignment({
      assignmentId: props.assignment.id!,
      title: props.assignment.title,
      description: props.assignment.description!,
      assignees: props.assignment.assignees,
      subdomainId: selected.value?.id,
      phaseId: props.assignment.phaseId,
      deadline: props.assignment.deadline,
      reviewer: props.assignment.reviewer
    }),
    () => {
      toast.add({
        title: 'Success',
        description: 'Successfully updated assignment'
      });

      emit('refresh');
    },
    (errorDescription) => {
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    }
  );
  isSubmitting.value = false;
}
</script>

<template>
  <UCard 
    :ui="{
      body: {
        base: 'flex justify-between items-center',
        padding: 'px-1 py-2 sm:p-2'
      }
    }"
  >
    <div class="flex gap-x-2 mr-4">
      <IssueNumber :assignment="assignment" />
      <p>{{ assignment.title }}</p>
    </div>
    <div class="flex gap-x-2">
      <USelectMenu
        v-model="selected"
        :options="subdomainOptions"
        option-attribute="id"
      >
        <template #label>
          <span class="truncate" v-if="selected">
            {{ selected.title }}
          </span>
          <span v-else>
            Select subdomain
          </span>
        </template>
        <template #option="{ option }">
          <UTooltip :text="option.description" :popper="{ position: 'top' }">
            <span>{{ option.title }}</span>
          </UTooltip>
        </template>
      </USelectMenu>
      <UButton 
        :loading="isSubmitting"
        label="Submit"
        @click="submit"
      />
    </div>
  </UCard>
</template>
