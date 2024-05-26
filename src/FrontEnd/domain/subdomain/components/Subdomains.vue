<script setup lang="ts">
import SubdomainForm from './SubdomainForm.vue';
import NewGiteaIssueList from '~/domain/assignment/components/NewGiteaIssueList.vue';
import { useSubdomainForm } from '../composable/useSubdomainForm';
import { useSubdomainTabs } from '../composable/useSubdomainTabs';
import { DeleteSubdomain, type Subdomain } from '../api/subdomainApi';

const props = defineProps<{
  projectId: string;
  numOfNewAssignment: number;
}>();

const route = useRoute();

const subdomainId = route.params.subdomainid?.toString();

const router = useRouter();

const tabs = useSubdomainTabs();

tabs.setProjectId(props.projectId);
tabs.setSubdomainId(subdomainId)

await tabs.fetch(true);

if (!subdomainId && tabs.currentSubdomain) {
  router.replace(`/project/${props.projectId}/${tabs.currentSubdomain.id}`);
}
else if (subdomainId && !tabs.currentSubdomain)
  router.replace(`/project/${props.projectId}`);

const form = useSubdomainForm(props.projectId);

const isFetching = ref(false);

const refreshKey = ref(0);
async function onRefresh() {
  let shouldRoute = false;

  if (!tabs.currentSubdomain)
    shouldRoute = true;

  isFetching.value = true;
  await tabs.fetch();
  isFetching.value = false;

  refreshKey.value++;

  if (shouldRoute && tabs.currentSubdomain)
    router.replace(`/project/${props.projectId}/${tabs.currentSubdomain.id}`);

  else if (subdomainId && !tabs.currentSubdomain)
    router.replace(`/project/${props.projectId}`);
}

function add() {
  form.create();
}

const editable = ref(false);

function enableEdit() {
  editable.value = true;
}

function disableEdit() {
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
  const currentTab = tabs.tabs[index];

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

    close();
    await onRefresh();

    if (tabs.tabs.length === 0)
      editable.value = false;
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

const isShowingNewAssignmentForm = ref(false);

function showNewAssignmentForm() {
  isShowingNewAssignmentForm.value = true;
}

function hideNewAssignmentForm() {
  isShowingNewAssignmentForm.value = false;
}
</script>

<template>
  <div class="py-5 h-full flex flex-col">
    <div class="flex flex-row gap-x-3 items-center">
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
      <UButton 
        v-if="numOfNewAssignment > 0"
        size="2xs"
        color="white"
        variant="ghost"
        @click="showNewAssignmentForm"
        label="New Assignment from Gitea"
      >
        <template #leading>
          <UBadge color="white">{{ numOfNewAssignment }}</UBadge>
        </template>
      </UButton>
    </div>
    <div class="flex flex-row flex-wrap gap-x-3 items-center mt-1">
      <UTabs 
        v-if="tabs.tabs.length > 0"
        :model-value="tabs.selectedTab" 
        @update:model-value="navigate"
        :items="tabs.tabs"
        :ui="{
          wrapper: 'space-y-0',
          list: {
            tab: {
              base: 'justify-start disabled:cursor-default disabled:opacity-100'
            }
          }
        }"
      >
        <template #default="{ item }">
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
            <UBadge 
              class="ml-2"
              color="gray"
              variant="solid"
              size="xs"
              v-else
            >
              {{ item.subdomain.numOfOpenedAssignments }}
            </UBadge>
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
      <NuxtPage :key="refreshKey" />
    </div>
  </div>

  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm()"
    prevent-close
  >
    <SubdomainForm :form="form" @submit="submit" />
  </UModal>

  <UModal 
    :model-value="isShowingNewAssignmentForm"
    @update:model-value="hideNewAssignmentForm"
    prevent-close
    :ui="{
      width: 'w-max sm:max-w-max'
    }"
  >
    <NewGiteaIssueList 
      :project-id="projectId"
      @close="hideNewAssignmentForm"
      :subdomain-tabs="tabs"
    />
  </UModal>
</template>
