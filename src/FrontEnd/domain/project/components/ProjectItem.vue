<script lang="ts" setup>
import type { ProjectItem } from '../api/projectApi';

const props = defineProps<{
  project: ProjectItem
}>();

const router = useRouter();

function routes() {
  router.push(`/project/${props.project.code}`);
}
</script>

<template>
  <UCard
    @click="routes"
    class="cursor-pointer"
    :ui="{
      background: 'dark:bg-gray-800',
      divide: 'dark:divide-gray-700',
      body: {
        padding: 'px-4 py-2 sm:p-3'
      },
      header: {
        padding: 'px-4 py-2 sm:px-3 sm:py-2'
      }
    }"
  >
    <div class="flex justify-between flex-col">
      <div class="flex flex-col mb-2">
        <div class="text-lg font-semibold">{{ project.name }}</div>
        <div class="text-sm text-gray-500">{{ project.code }}</div>
      </div>

      <div class="space-x-1">
        <UTooltip
          text="Number of Open Assignments"
        >
          <UBadge :label="project.numOfOpenAssignment" color="red" />
        </UTooltip>

        <UTooltip
          text="Number of Working Assignments"
        >
          <UBadge :label="project.numOfWorkingAssignment" color="blue" />
        </UTooltip>

        <UTooltip
          text="Number of Waiting Review Assignments"
        >
          <UBadge :label="project.numOfWaitingReviewAssignment" color="yellow" />
        </UTooltip>

        <UTooltip
          text="Number of Completed Assignments"
        >
          <UBadge :label="project.numOfCompletedAssignment" color="primary" />
        </UTooltip>
      </div>
    </div>
  </UCard>
</template>
