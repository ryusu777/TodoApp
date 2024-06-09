<script setup lang="ts">
import { useHierarchyForm } from '~/domain/project/composable/useHierarchyForm';
import { DeleteProjectHierarchy, SyncProjectMembers, type Hierarchy } from '../api/projectApi';
import HierarchyForm from './HierarchyForm.vue';
import ProjectHierarchy from './ProjectHierarchy.vue';
import { useProject } from '../composable/useProject';
import NewMemberList from './NewMemberList.vue';

// component definitions
const props = defineProps<{
  pending: boolean;
  refresh: () => Promise<void>;
}>();

// project data
const project = useProject();

// component list
const Components = computed(() => {
  return project.hierarchies!.map(hierarchy => h(ProjectHierarchy, {
    hierarchyId: hierarchy.id,
    pending: props.pending,
    onRefresh: props.refresh,
    key: hierarchy.memberUsernames.join('-')
  }));
});

const ActiveHierarchy = computed(() => Components.value[selectedTab.value])

// utils
const apiUtils = useApiUtils();
const toast = useToast();

// project hierarchy api functions
const syncing = ref(false);

async function sync() {
  syncing.value = true;
  await apiUtils.try(() => SyncProjectMembers({
      projectId: project.project!.id
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

async function deleteHierarchy(item: typeof tabs.value[number], close: () => void) {
  await apiUtils.try(() => DeleteProjectHierarchy(project.project!.id, item.id),
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

  selectedTab.value = 0;
  close();
}

// tabs functions
const tabs = computed(() => {
  return project.hierarchies!.map(hierarchy => {
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
const form = useHierarchyForm(project.project!.id);


// unassigned member role functions
const showingAssignMembersRoleForm = ref(false);
const unassignedMember = computed(() => project
  .members!
  .filter(username => !project.hierarchies!
    .some(hierarchy => hierarchy.memberUsernames.includes(username))));

function showAssignMembersRoleForm() {
  showingAssignMembersRoleForm.value = true;
}
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
      <UButton 
        v-if="unassignedMember.length > 0"
        size="2xs"
        color="white"
        variant="ghost"
        @click="showAssignMembersRoleForm"
        label="New member that don't have roles"
      >
        <template #leading>
          <UBadge color="white">{{ unassignedMember.length }}</UBadge>
        </template>
      </UButton>
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
                @click="form.update(project.hierarchies!.find(e => e.id === item.id)!)"
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
                        @click="deleteHierarchy(item, close)"
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
          size="md"
          icon="heroicons:x-mark-16-solid"
          color="red"
          :ui="{
            rounded: 'rounded-xl'
          }"
          @click="editable = false"
        />
      </div>
    </div>
    <component :is="ActiveHierarchy" class="mt-3" />
  </div>
  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm"
    prevent-close
  >
    <HierarchyForm 
      :form="form"
      @refresh="props.refresh"
    />
  </UModal>
  <UModal
    v-model="showingAssignMembersRoleForm"
    prevent-close
  >
    <NewMemberList
      :unassigned-members="unassignedMember"
      @close="showingAssignMembersRoleForm = false"
      @refresh="props.refresh"
    />
  </UModal>
</template>
