<script setup lang="ts">
import { useHierarchyForm } from '~/domain/project/composable/useHierarchyForm';
import { DeleteProjectHierarchy, SyncProjectMembers, type Hierarchy, type Member } from '../api/projectApi';
import HierarchyForm from './HierarchyForm.vue';

// component definitions
const props = defineProps<{
  hierarchies: Hierarchy[];
  projectId: string;
  pending: boolean;
  refresh: () => Promise<void>;
}>();

// utils
const apiUtils = useApiUtils();
const toast = useToast();

// project hierarchy api functions
const syncing = ref(false);

async function sync() {
  syncing.value = true;
  await apiUtils.try(() => SyncProjectMembers({
      projectId: props.projectId
    }),
    () => {
      syncing.value = false;
      toast.add({
        title: 'Success',
        description: 'Successfully synced project members with Gitea Repositories assignees'
      });
      props.refresh();
    },
    (errorDescription) => {
      syncing.value = false;
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    })
}

async function deleteHierarchy(item: typeof tabs.value[number]) {
  await apiUtils.try(() => DeleteProjectHierarchy(props.projectId, item.id),
    () => {
      toast.add({
        title: 'Success',
        description: 'Successfully deleted hierarchy'
      });
      props.refresh();
    },
    (errorDescription) => {
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    });
}

// tabs functions
const tabs = computed(() => {
  return props.hierarchies.map(hierarchy => {
    return {
      label: hierarchy.name,
      id: hierarchy.id,
      disabled: editable.value
    }
  });
});

const selectedTab = ref(0);

// form functions
const editable = ref(false);
const form = useHierarchyForm(props.projectId);
</script>

<template>
  <div class="py-5">
    <div class="flex flex-row gap-3"> 
      <span class="text-lg font-bold">Project Members</span>
      <UButton 
        icon="heroicons:link-16-solid"
        label="Sync"
        @click="sync"
        :loading="syncing"
      />
    </div>
    <div class="flex flex-row flex-wrap gap-x-3 items-center mt-1">
      <UTabs
        v-if="tabs.length"
        v-model="selectedTab"
        :items="tabs"
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
                        @click="deleteHierarchy(item)"
                      />
                    </div>
                  </div>
                </template>
              </UPopover>
            </div>
          </div>
        </template>
      </UTabs>
      <div class="space-x-2" v-if="!editable">
        <UButton
          size="md"
          icon="heroicons:pencil"
          color="gray"
          :ui="{
            rounded: 'rounded-xl'
          }"
          @click="editable = true"
        />
        <UButton
          size="md"
          icon="heroicons:plus"
          color="gray"
          :ui="{
            rounded: 'rounded-xl'
          }"
          @click="form.create"
        />
        <UButton
          color="gray"
          size="md"
          icon="heroicons:arrow-path-rounded-square" 
          :ui="{
            rounded: 'rounded-xl'
          }"
          @click="props.refresh"
        />
      </div>
      <div class="space-x-2" v-if="editable">
        <UButton
          size="sm"
          icon="heroicons:check"
          label="Save Changes"
          color="green"
          @click="editable = false"
        />
        <UButton
          size="sm"
          icon="heroicons:x-mark"
          label="Cancel"
          color="red"
          @click="editable = false"
        />
      </div>
    </div>
  </div>
  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm"
    prevent-close
  >
    <HierarchyForm 
      :form="form"
      :hierarchies="hierarchies"
      @refresh="props.refresh"
    />
  </UModal>
</template>
