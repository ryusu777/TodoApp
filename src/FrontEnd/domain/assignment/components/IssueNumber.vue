<script setup lang="ts">
import type { NumberedAssignment } from '../composables/useAssignmentState';

defineProps<{
  assignment: NumberedAssignment
}>();
</script>

<template>
  <UTooltip :text="assignment.issueUrl" :popper="{ placement: 'top' }">
    <a :href="assignment.issueUrl" target="_blank" class="text-primary underline">#{{ assignment.issueNumber }}</a>
  </UTooltip>
</template>
