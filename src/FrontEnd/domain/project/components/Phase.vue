<script setup lang="ts">
import { type Phase } from "~/domain/project/api/projectApi";
import { useDateFormat } from "@vueuse/core";

const props = defineProps<{
  phase: Phase;
  editable?: boolean;
}>();

const startDate = computed(() => useDateFormat(props.phase.startDate, 'Do MMM').value);
const endDate = computed(() => useDateFormat(props.phase.endDate, 'Do MMM').value);

const emit = defineEmits(['update', 'delete']);

</script>

<template>
  <UCard
    :ui="{
      background: 'dark:bg-gray-800',
      divide: 'dark:divide-gray-700',
      body: {
        padding: 'px-4 py-3 sm:p-5'
      },
      header: {
        padding: 'px-4 py-3 sm:px-4 sm:py-3'
      }
    }"
  >
    <template #header>
      <div class="flex flex-col">
        <div class="flex flex-row justify-between">
          <p class="text-bold text-xl">{{ props.phase.name }}</p>
          <div class="flex gap-1">
            <UButton 
              variant="solid" 
              size="xs"
              square
              color="red"
              v-if="editable"
              @click="emit('delete')"
              icon="heroicons:trash"
            />
            <UButton 
              variant="solid" 
              size="xs"
              square
              v-if="editable"
              @click="emit('update')"
              icon="heroicons:pencil"
            />
          </div>
        </div>
        <p class="text-sm ">{{ startDate }} - {{ endDate }}</p>
      </div>
    </template>

    <p>{{ phase.description }}</p>
  </UCard>
</template>
