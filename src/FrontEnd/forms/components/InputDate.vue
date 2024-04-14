<script setup lang="ts">
import { useDateFormat } from '@vueuse/core';
import DatePicker from './DatePicker.vue';

const props = defineProps<{
  modelValue?: string;
}>();

const emit = defineEmits(['update:modelValue']);
const formatted = computed(() => {
  return props.modelValue !== undefined ? useDateFormat(props.modelValue, 'DD MMM, YYYY').value : 'dd-mm-yyyy';
});
    

function update(value: Date) {
  const valueFormatted = useDateFormat(value, 'YYYY-MM-DD');
  emit('update:modelValue', valueFormatted.value);
}
</script>

<template>
  <UPopover :popper="{ placement: 'bottom-start' }">
    <UButton 
      color="white"
      icon="i-heroicons-calendar-days-20-solid" 
      :label="formatted" 
    />

    <template #panel="{ close }">
      <DatePicker 
        :model-value="modelValue || ''" 
        @update:model-value="update" 
        @close="close" 
      />
    </template>
  </UPopover>
</template>
