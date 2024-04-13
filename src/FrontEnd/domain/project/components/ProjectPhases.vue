<script setup lang="ts">
import type { Phase } from '../api/projectApi';
import PhaseVue from './Phase.vue';

defineProps<{
  phases: Phase[]
}>();
</script>

<template>
  <div class="py-5">
    <h1>Project phase</h1>
    <div class="flex flex-row gap-3 mt-3">
      <div v-for="phase in phases" style="width: 20%; min-height: max-content;">
        <PhaseVue :phase="phase" class="h-full" /> 
      </div>
    </div>
  </div>
</template>
