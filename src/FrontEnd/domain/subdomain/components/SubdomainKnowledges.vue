<script setup lang="ts">
import type { SubdomainKnowledge } from '../api/subdomainApi';
import { useSubdomainKnowledgeForm } from '../composable/useSubdomainKnowledgeForm';
import SubdomainKnowledgeForm from './SubdomainKnowledgeForm.vue';
import SubdomainKnowledgeVue from './SubdomainKnowledge.vue';

const props = defineProps<{
  subdomainId: string;
  knowledges: SubdomainKnowledge[];
}>();

const tabs = computed(() => props.knowledges.map(e => {
  return {
    label: e.title
  }
}));

const selectedTab = ref(0);

const selectedKnowledge = computed(() => props.knowledges[selectedTab.value]);

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
    emit('refresh');
  }
}

</script>

<template>
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
              base: 'justify-start',

            }
          },
        }"
      />
      <div class="px-1 pb-1">
        <UButton
          size="sm"
          variant="ghost"
          icon="heroicons:plus"
          color="white"
          class="w-full justify-center"
          @click="form.create()"
        />
      </div>
    </div>
    <div class="flex-grow pl-5">
      <SubdomainKnowledgeVue 
        :knowledge="selectedKnowledge" 
        @submit="submit" 
        :subdomain-id="subdomainId"
      />
    </div>
  </div>

  <UModal 
    :model-value="form.show.value" 
    @update:model-value="form.closeForm()"
    prevent-close
  >
    <SubdomainKnowledgeForm :form="form" @submit="submit" />
  </UModal>

</template>
