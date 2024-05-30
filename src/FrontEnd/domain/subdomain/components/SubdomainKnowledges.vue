<script setup lang="ts">
import { DeleteSubdomainKnowledge, type SubdomainKnowledge } from '../api/subdomainApi';
import { useSubdomainKnowledgeForm } from '../composable/useSubdomainKnowledgeForm';
import SubdomainKnowledgeForm from './SubdomainKnowledgeForm.vue';
import SubdomainKnowledgeVue from './SubdomainKnowledge.vue';
import { createReusableTemplate } from '@vueuse/core';

const props = defineProps<{
  subdomainId: string;
  knowledges: SubdomainKnowledge[];
}>();

const disableTabs = ref(false);

const tabs = computed(() => props.knowledges.map(e => {
  return {
    label: e.title,
    disabled: disableTabs.value,
    knowledge: {
      id: e.id,
      title: e.title,
      content: e.content
    }
  }
}));

const refreshTabKey = ref(0);

const refreshKnowledgeKey = ref(0);

const selectedTab = ref(0);

const selectedKnowledge = computed(() => props.knowledges[selectedTab.value]);

watch(() => selectedKnowledge.value, (val) => {
  if (!val && selectedTab.value === 0) {
    refreshTabKey.value++;
    disableEdit();
  }
  else if (!val) {
    selectedTab.value = 0;
  }

  refreshKnowledgeKey.value++;
});

watch(() => selectedTab.value, () => refreshTabKey.value++);

const form = useSubdomainKnowledgeForm(props.subdomainId);

const toast = useToast();

const emit = defineEmits(['refresh']);

async function submit() {
  const errorMessage = await form.submit();
  if (errorMessage)
    toast.add({
      title: 'Error',
      description: errorMessage,
      color: 'red'
    });
  else {
    toast.add({
      title: 'Success',
      description: 'Successfully created subdomain knowledge'
    });

    form.closeForm();
    refresh();
  }
}

function refresh() {
  emit('refresh');
}

const editable = ref(false);

function enableEdit() {
  editable.value = true;
}

function disableEdit() {
  editable.value = false;
}

async function doDelete({ knowledge }: { knowledge: SubdomainKnowledge }, close: () => void) {
  if (!knowledge.id) {
    toast.add({
      title: 'Error',
      description: 'Data does not exist, please refresh the page',
      color: 'red'
    });
    return;
  }

  try {
    await DeleteSubdomainKnowledge({
      knowledgeId: knowledge.id,
      subdomainId: props.subdomainId
    });

    toast.add({
      title: 'Success',
      description: 'Successfully deleted knowledge'
    })

    close();
    refresh();
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

const [DefineTemplate, ReuseTemplate] = createReusableTemplate();

defineExpose({
  ReuseTemplate
});
</script>

<template>
  <DefineTemplate>
    <div>
      <UButton 
        icon="heroicons:plus"
        label="New Knowledge"
        size="md"
        @click="form.create()"
      />
    </div>
  </DefineTemplate>
  <div class="flex flex-row flex-grow pt-2">
    <div class="bg-gray-100 dark:bg-gray-800 rounded-lg">
      <UTabs 
        v-model="selectedTab"
        :items="tabs"
        orientation="vertical"
        :ui="{
          wrapper: 'min-w-[200px] max-w-[200px]',
          list: {
            background: 'bg-transparent dark:bg-transparent',
            padding: 'p-0 px-1 pt-1',
            tab: {
              base: 'justify-start disabled:cursor-default disabled:opacity-100',

            }
          },
        }"
        :key="refreshTabKey"
      >
        <template #default="{ item }">
          <div class="flex flex-row w-full justify-between items-center">
            <span>{{ item.label }}</span>
            <div v-if="editable" class="gap-x-1 ml-2 flex">
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
      <div class="px-1">
        <UButton 
          size="sm"
          variant="ghost"
          color="red"
          class="w-full justify-center"
          @click="enableEdit"
          icon="heroicons:trash"
          v-if="!editable"
        />
        <UButton 
          size="sm"
          variant="ghost"
          color="red"
          @click="disableEdit"
          class="w-full justify-center"
          icon="heroicons:x-mark-16-solid"
          v-if="editable"
        />
      </div>
    </div>
    <div class="flex-grow pl-5">
      <SubdomainKnowledgeVue 
        @refresh="refresh"
        :key="refreshKnowledgeKey"
        :knowledge="selectedKnowledge" 
        :subdomain-id="subdomainId"
      />
    </div>
  </div>

  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm()"
    prevent-close
  >
    <SubdomainKnowledgeForm 
      :form="form" 
      @submit="submit" 
    />
  </UModal>

</template>
