<script setup lang="ts">
import SubdomainForm from './SubdomainForm.vue';
import { useSubdomainForm } from '../composable/useSubdomainForm';
import { useSubdomainTabs } from '../composable/useSubdomainTabs';
import type { Subdomain } from '../api/subdomainApi';

const props = defineProps<{
  projectId: string;
}>();

const tabs = useSubdomainTabs(props.projectId);

await tabs.fetch();

const form = useSubdomainForm(props.projectId);

const isFetching = ref(false);

async function onRefresh() {
  isFetching.value = true;
  await tabs.fetch();
  isFetching.value = false;
}

function update(subdomain: Subdomain) {
  form.update(subdomain);
}

function add() {
  form.create();
}

const editable = ref(false);

const toast = useToast();

function cancel() {
  form.closeForm();
}

async function submit() {
  const error = await form.submit();

  if (error)
    toast.add({
      title: 'Error',
      description: error,
      color: 'red'
    });
  else {
    toast.add({
      title: 'Success',
      description: 'Subdomain successfully added'
    });
    form.closeForm();
    await onRefresh();
  }
}
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Project's Subdomain</span>
      <div v-if="!editable">
        <UButton 
          icon="heroicons:arrow-path-rounded-square"
          size="xs"
          color="white"
          variant="ghost"
          @click="onRefresh"
          :loading="isFetching"
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
    </div>
    <div class="flex flex-row flex-wrap gap-3 mt-3 items-center">
      <UTabs 
        v-if="tabs.tabs.value.length > 0"
        :model-value="tabs.selectedTab.value" 
        @update:model-value="tabs.setTab"
        :items="tabs.tabs.value"
        :ui="{
          wrapper: 'space-y-0'
        }"
      />
      <div>
        <UButton
          size="md"
          icon="heroicons:plus"
          @click="add"
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
    <SubdomainForm :form="form" @submit="submit" />
  </UModal>
</template>
