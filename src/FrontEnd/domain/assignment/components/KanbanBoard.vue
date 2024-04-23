<script setup lang="ts">
import type { Assignment } from '../api/assignmentApi';
import AssignmentVue from './Assignment.vue';

defineProps<{
  assignments: Assignment[];
  type: string;
}>();

</script>
<template>
  <div class="max-w-[250px] flex flex-col gap-y-2">
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
    <AssignmentVue :assignment="assignment" v-for="assignment in assignments"/>
  </div>
</template>
