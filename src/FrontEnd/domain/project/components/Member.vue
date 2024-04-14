<script setup lang="ts">
import { type Member } from "~/domain/project/api/projectApi";

const props = defineProps<{
  member: Member;
  editable?: boolean;
}>();

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
    <div class="flex flex-col">
      <div class="flex flex-row justify-between gap-3">
        <p class="text-bold text-xl">{{ props.member.username }}</p>
        <div class="flex gap-1" :class="!editable ? 'invisible' : ''">
          <UButton 
            variant="solid" 
            size="xs"
            square
            color="red"
            @click="emit('delete')"
            icon="heroicons:trash"
          />
          <UButton 
            variant="solid" 
            size="xs"
            square
            @click="emit('update')"
            icon="heroicons:pencil"
          />
        </div>
      </div>
    </div>
  </UCard>
</template>
