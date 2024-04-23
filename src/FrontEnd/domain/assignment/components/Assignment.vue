<script setup lang="ts">
import type { Assignment } from '../api/assignmentApi';

defineProps<{
  assignment: Assignment
}>();
</script>
<template>
  <UCard
    :ui="{
      body: {
        padding: 'px-3 py-3 sm:px-3 sm:py-3'
      },
      footer: {
        padding: 'px-3 py-3 sm:px-3 sm:py-3'
      }
    }"
  >
    <div class="flex flex-col gap-y-2">
      <div class="flex justify-between">
        <p class="text-md">{{ assignment.title }}</p>
        <UAvatarGroup size="sm" :max="2">
          <UAvatar 
            v-for="assignee in assignment.assignees"
            :alt="assignee" 
            size="sm" 
          />
        </UAvatarGroup>
      </div>
      <p class="text-sm">{{ assignment.description }}</p>
    </div>
    <div>
      <span class="text-xs text-gray-200">Reviewer</span>
      <div class="flex gap-x-2 items-center">
        <UAvatar :alt="assignment.reviewer" size="sm" />
        <p class="text-sm">{{ assignment.reviewer }}</p>
      </div>
    </div>
  </UCard>
</template>
