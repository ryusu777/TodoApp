<script lang="ts" setup>
import type { useSubdomainTabs } from '~/domain/subdomain/composable/useSubdomainTabs';
import { useAssignmentState } from '../composables/useAssignmentState';
import NewGiteaIssue from './NewGiteaIssue.vue';

const props = defineProps<{
  projectId: string;
  subdomainTabs: ReturnType<typeof useSubdomainTabs>
}>();

const assignmentState = useAssignmentState(props.projectId, '');

const assignments = assignmentState.assignments;

onMounted(() => assignmentState.fetch(false));

const emit = defineEmits(['close']);

function close() {
  emit('close');
}
</script>

<template>
  <div class="flex flex-col gap-y-2 p-5">
    <div class="flex justify-between">
      <div>
        <h1>New Assignment</h1>
        <p class="text-wrap" v-if="assignmentState.assignments.value.length > 0">
          These assignment were created from Gitea hooks, <br />
          please give them subdomain for them to stand upon :D
        </p>
        <p class="text-wrap" v-else>
          Apparently, all assignment has subdomains..<br />
          Good work so far :D <br />
          You can close this dialog safely..
        </p>
      </div>
      <div>
        <UButton 
          size="xl"
          icon="heroicons:x-mark-16-solid"
          color="red"
          variant="ghost"
          @click="close"
        />
      </div>
    </div>
    <USkeleton class="h-10 w-full" v-if="!assignmentState.isFetching" />
    <USkeleton class="h-10 w-full" v-if="!assignmentState.isFetching" />
    <NewGiteaIssue
      v-for="assignment in assignments"
      :assignment="assignment"
      :subdomain-tabs="subdomainTabs"
      :project-id="projectId"
      @refresh="assignmentState.fetch(false)"
    />
  </div>
</template>
