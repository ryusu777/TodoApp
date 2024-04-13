<script setup lang="ts">
import type { Phase } from '../api/projectApi';
import { usePhaseForm } from '../composable/usePhaseForm';
import PhaseVue from './Phase.vue';
import PhaseForm from './PhaseForm.vue';

const props = defineProps<{
  phases: Phase[];
}>();

const form = usePhaseForm(props.phases);

function update(phase: Phase) {
  form.setModel(phase);
  form.showForm();
}

const editable = ref(false);

function edit() {
  editable.value = true;
}
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Project phase</span>
      <UButton 
        icon="heroicons:pencil"
        size="xs"
        color="white"
        variant="ghost"
        @click="edit"
        v-if="editable === false"
      />
      <UButton 
        size="xs"
        color="red"
        label="Cancel"
        @click="editable = false"
        v-if="editable === true"
        :ui="{
          font: 'font-bold'
        }"
      />
      <UButton 
        size="xs"
        label="Save Changes"
        @click="editable = false"
        v-if="editable === true"
        :ui="{
          font: 'font-bold'
        }"
      />
    </div>
    <div class="flex flex-row gap-3 mt-3">
      <div v-for="phase in phases" style="width: 20%; min-height: max-content;">
        <PhaseVue 
          :phase="phase" 
          :editable="editable"
          class="h-full" 
          @update="update(phase)" 
        /> 
      </div>
    </div>
  </div>

  <UModal :model-value="form.show.value" @update:model-value="form.closeForm()">
    <PhaseForm :form="form" />
  </UModal>
</template>
