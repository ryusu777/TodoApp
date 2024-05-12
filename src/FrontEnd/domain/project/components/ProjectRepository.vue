<script lang="ts" setup>
import type { GiteaRepository } from '~/domain/gitea-integration/api/giteaIntegrationApi';

const props = defineProps<{
  repository: GiteaRepository,
  editable?: boolean
}>();

const emit = defineEmits(['delete']);
</script>

<template>
  <UCard
    :ui="{
      background: 'dark:bg-gray-800',
      divide: 'dark:divide-gray-700',
      body: {
        padding: 'px-3 py-1 sm:p-3'
      },
      header: {
        padding: 'px-3 py-1 sm:px-3 sm:py-1'
      }
    }"
  >
    <div class="flex flex-col">
      <div class="flex flex-row justify-between gap-3">
        <p class="text-bold text-xl">{{ props.repository.repoOwner }}/{{ props.repository.repoName}}</p>
        <div class="flex gap-1" :class="!editable ? 'invisible' : ''">
          <UButton 
            variant="solid" 
            size="xs"
            square
            color="red"
            @click="emit('delete')"
            icon="heroicons:trash"
          />
        </div>
      </div>
    </div>
  </UCard>
</template>
