<script setup lang="ts">
import { SyncProjectMembers, type Member } from '../api/projectApi';
import { useMemberForm } from '../composable/useMemberForm';
import MemberVue from './Member.vue';

const props = defineProps<{
  members: Member[];
  projectId: string;
  pending: boolean;
  refresh: () => Promise<void>;
}>();

const form = useMemberForm(props.members, props.projectId);
const apiUtil = useApiUtils();
const syncing = ref(false);

async function onRefresh() {
  await props.refresh();

  form.refresh(props.members);
}

async function sync() {
  syncing.value = true;
  await apiUtil.try(() => SyncProjectMembers({
      projectId: props.projectId
    }),
    () => {
      syncing.value = false;
      toast.add({
        title: 'Success',
        description: 'Successfully synced project members with Gitea Repositories assignees'
      });
      onRefresh();
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

const toast = useToast();

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
    <div class="flex flex-row flex-wrap gap-3 mt-3">
      <div v-for="member in form.members.value" style="min-width: max-content; max-height: max-content;">
        <MemberVue
          :member="member" 
          class="h-full" 
        />
      </div>
    </div>
  </div>
</template>
