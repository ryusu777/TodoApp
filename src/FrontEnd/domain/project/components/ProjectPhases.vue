<script setup lang="ts">
import type { Phase } from '../api/projectApi';
import { usePhaseForm } from '../composable/usePhaseForm';
import PhaseVue from './Phase.vue';
import PhaseForm from './PhaseForm.vue';

const props = defineProps<{
  phases: Phase[];
  projectId: string;
  pending: boolean;
  refresh: () => Promise<void>;
}>();

const form = usePhaseForm(props.phases, props.projectId);

async function onRefresh() {
  await props.refresh();

  form.refresh(props.phases);
}

function update(phase: Phase) {
  form.setModel(phase);
  form.showForm();
}

function add() {
  form.setModel({} as Phase);
  form.showForm();
}

const editable = ref(false);

function edit() {
  editable.value = true;
}

const toast = useToast();

async function persist() {
  const result = await form.persist();

  if (result?.errorDescription) {
    toast.add({ title: 'Error', description: result?.errorDescription });
    return;
  }

  editable.value = false;
  toast.add({ title: 'Success', description: 'Successfully updated phases' });
}

function cancel() {
  form.revert();
  editable.value = false;
}

function remove(phase: Phase) {
  form.remove(phase);
}
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Project phase</span>
      <div v-if="!editable">
        <UButton 
          icon="heroicons:pencil"
          size="xs"
          color="white"
          variant="ghost"
          @click="edit"
        />
        <UButton 
          icon="heroicons:arrow-path-rounded-square"
          size="xs"
          color="white"
          variant="ghost"
          @click="onRefresh"
          :loading="pending"
        />
      </div>
      <UButton 
        size="xs"
        color="red"
        label="Cancel"
        @click="cancel"
        v-if="editable === true"
        :ui="{
          font: 'font-bold'
        }"
      />
      <UButton 
        size="xs"
        label="Save Changes"
        @click="persist"
        v-if="editable === true"
        :ui="{
          font: 'font-bold'
        }"
      />
    </div>
    <div class="flex flex-row gap-3 mt-3">
      <div v-for="phase in form.phases.value" style="width: 20%; min-height: max-content;">
        <PhaseVue 
          :phase="phase" 
          :editable="editable"
          class="h-full" 
          @update="update(phase)" 
          @delete="remove(phase)"
        />
      </div>
      <div class="flex items-center">
        <UButton 
          size="xl"
          icon="heroicons:plus"
          @click="add"
          v-if="editable === true"
          color="gray"
          :ui="{
            rounded: 'rounded-xl'
          }"
        />
      </div>
    </div>
  </div>

  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm()"
    prevent-close
  >
    <PhaseForm :form="form" />
  </UModal>
</template>
