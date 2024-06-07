<script setup lang="ts">
const props = defineProps<{
  username: string;
}>();

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
        <p class="text-bold text-xl">{{ props.username }}</p>
      </div>
    </div>
  </UCard>
</template>
