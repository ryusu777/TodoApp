<script setup lang="ts">
import SubdomainForm from './SubdomainForm.vue';
import { useSubdomainForm } from '../composable/useSubdomainForm';
import { useSubdomainTabs } from '../composable/useSubdomainTabs';
import { DeleteSubdomain, type Subdomain } from '../api/subdomainApi';
import { FetchError } from 'ofetch';

const props = defineProps<{
  projectId: string;
}>();

const route = useRoute();

const subdomainId = route.params.subdomainid?.toString();

const router = useRouter();

const tabs = useSubdomainTabs(props.projectId, subdomainId);

await tabs.fetch(true);

if (!subdomainId) {
  router.push(`/project/${props.projectId}/${tabs.currentSubdomain.value.id}`);
}

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

function disableEdit() {
  tabs.enableTabs();
  editable.value = false;
}

const toast = useToast();

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

async function doDelete({ subdomain }: { subdomain: Subdomain }, close: () => void) {
  if (!subdomain.id) {
    toast.add({
      title: 'Error',
      description: 'Data does not exist, please refresh the page',
      color: 'red'
    });
    return;
  }

  try {
    await DeleteSubdomain({
      subdomainId: subdomain.id
    });

    toast.add({
      title: 'Success',
      description: 'Successfully deleted subdomain'
    })

    await onRefresh();
  } catch (e: any) {
    if ('data' in e) {
      toast.add({
        title: 'Error',
        description: e.data.errorDescription || 'Something went wrong, please try again later',
        color: 'red'
      });
    }
  }
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
        v-if="!editable"
      />
      <UButton 
        size="2xs"
        variant="ghost"
        color="red"
        @click="disableEdit"
        icon="heroicons:x-mark-16-solid"
        v-if="editable"
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
            <div v-if="editable" class="gap-x-1 ml-2 flex">
              <UButton 
                icon="heroicons:pencil"
                square
                size="2xs"
                @click="edit(item)"
                v-if="editable" 
              />
              <UPopover>
                <UButton 
                  icon="heroicons:trash"
                  square
                  color="red"
                  size="2xs"
                  v-if="editable" 
                />

                <template #panel="{ close }">
                  <div class="flex flex-col p-3 gap-y-2 text-white">
                    <span>Are you sure want to delete this?</span>
                    <div class="flex justify-end gap-x-1">
                      <UButton 
                        icon="heroicons:x-mark-16-solid"
                        label="No"
                        square
                        size="2xs"
                        class="px-2"
                        @click="close"
                      />
                      <UButton 
                        icon="heroicons:trash"
                        label="Yes"
                        square
                        color="red"
                        size="2xs"
                        class="px-2"
                        @click="doDelete(item, close)"
                      />
                    </div>
                  </div>
                </template>
              </UPopover>
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
