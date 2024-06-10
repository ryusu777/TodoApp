<script lang="ts" setup>
import { isDeleteExpression } from 'typescript';
import { DeleteProject, type ProjectItem } from '../api/projectApi';

const props = defineProps<{
  project: ProjectItem
}>();

const router = useRouter();

const apiUtils = useApiUtils();

const toast = useToast();

const emit = defineEmits(['refresh']);

const isDeleting = ref(false);
function doDelete(close: () => void) {
  isDeleting.value = true;
  apiUtils.try(() => DeleteProject(props.project.code), 
    () => {
      isDeleting.value = false;
      toast.add({
        title: 'Success',
        description: 'Project deleted successfully',
      });
      close();
      emit('refresh');
    }, (errorDescription) => {
      isDeleting.value = false;
      toast.add({
        title: 'Error',
        description: errorDescription,
        color: 'red'
      });
    });
}

function routes() {
  router.push(`/project/${props.project.code}`);
}
</script>

<template>
  <UCard
    :ui="{
      background: 'dark:bg-gray-800',
      divide: 'dark:divide-gray-700',
      body: {
        padding: 'px-4 py-2 sm:p-3'
      },
      header: {
        padding: 'px-4 py-2 sm:px-3 sm:py-2'
      }
    }"
  >
    <div class="flex justify-between flex-col">
      <div class="flex justify-between gap-x-2">
        <div class="flex flex-col mb-2">
          <div class="text-lg font-semibold">{{ project.name }}</div>
          <div class="text-sm text-gray-500">{{ project.code }}</div>
        </div>
        <UPopover>
          <UButton
            class="w-full"
            label="Delete"
            icon="heroicons:trash"
            color="gray"
            size="xs"
          />
          <template #panel="{ close: closeDelete }">
            <div class="flex flex-col p-3 gap-y-2 text-white">
              <span>Are you sure want to delete this?</span>
              <div class="flex justify-end gap-x-1">
                <UButton 
                  icon="heroicons:x-mark-16-solid"
                  label="No"
                  square
                  size="2xs"
                  class="px-2"
                  @click="closeDelete"
                />
                <UButton 
                  icon="heroicons:trash"
                  label="Yes"
                  square
                  color="red"
                  size="2xs"
                  class="px-2"
                  :loading="isDeleting"
                  @click="doDelete(closeDelete)"
                />
              </div>
            </div>
          </template>
        </UPopover>
      </div>

      <div class="flex gap-x-2 justify-between">
        <div class="space-x-1">
          <UTooltip
            text="Number of Open Assignments"
          >
            <UBadge :label="project.numOfOpenAssignment" color="red" />
          </UTooltip>

          <UTooltip
            text="Number of Working Assignments"
          >
            <UBadge :label="project.numOfWorkingAssignment" color="blue" />
          </UTooltip>

          <UTooltip
            text="Number of Waiting Review Assignments"
          >
            <UBadge :label="project.numOfWaitingReviewAssignment" color="yellow" />
          </UTooltip>

          <UTooltip
            text="Number of Completed Assignments"
          >
            <UBadge :label="project.numOfCompletedAssignment" color="primary" />
          </UTooltip>
        </div>
        <div>
          <UTooltip text="Go to project page">
            <UButton 
              icon="heroicons:arrow-right"
              @click="routes"
            />
          </UTooltip>
        </div>
      </div>
    </div>
  </UCard>
</template>
