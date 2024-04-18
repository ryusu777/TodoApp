<script setup lang="ts">
import SubdomainForm from './SubdomainForm.vue';
import { useSubdomainForm } from '../composable/useSubdomainForm';
import { useSubdomainTabs } from '../composable/useSubdomainTabs';
import type { Subdomain } from '../api/subdomainApi';

const props = defineProps<{
  projectId: string;
}>();

const route = useRoute();

const subdomainId = route.params.subdomainid.toString();

const router = useRouter();

const tabs = useSubdomainTabs(props.projectId, subdomainId);

await tabs.fetch(true);

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

function enableEdit() {
  tabs.disableTabs();
  editable.value = true;
}

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

function navigate(index: number) {
  const currentTab = tabs.tabs.value[index];

  router.push(currentTab.to);

  tabs.setTab(index);
}

function edit({ subdomain }: { subdomain: Subdomain }) {
  form.update(subdomain);
}
</script>

<template>
  <div class="py-5 h-full flex flex-col">
    <div class="flex flex-row gap-x-3 items-end">
      <span class="text-md font-bold">Subdomain</span>
      <UButton 
        size="2xs"
        variant="ghost"
        color="white"
        @click="enableEdit"
        icon="heroicons:pencil"
      />
    </div>
    <div class="flex flex-row flex-wrap gap-x-3 items-center mt-1">
      <UTabs 
        v-if="tabs.tabs.value.length > 0"
        :model-value="tabs.selectedTab.value" 
        @update:model-value="navigate"
        :items="tabs.tabs.value"
        :ui="{
          wrapper: 'space-y-0',
          list: {
            tab: {
              base: 'justify-start disabled:cursor-default disabled:opacity-100'
            }
          }
        }"
      >
        <template #default="{ item, index, selected }">
          <div class="flex flex-row w-full justify-between items-center">
            <span>{{ item.label }}</span>
            <div v-if="editable" class="space-x-1 ml-2">
              <UButton 
                icon="heroicons:pencil"
                square
                size="2xs"
                @click="edit(item)"
                v-if="editable" 
              />
              <UButton 
                icon="heroicons:trash"
                square
                color="red"
                size="2xs"
                v-if="editable" 
              />
            </div>
          </div>
        </template>
      </UTabs>
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
      <div>
        <UButton 
          icon="heroicons:arrow-path-rounded-square"
          size="xs"
          color="white"
          variant="ghost"
          @click="onRefresh"
          :loading="isFetching"
        />
      </div>
    </div>

    <div class="mt-3 flex-grow">
      <NuxtPage />
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
