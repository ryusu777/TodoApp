<script setup lang="ts">
import { useEditor, EditorContent } from '@tiptap/vue-3';
import { UpdateSubdomainKnowledge, type SubdomainKnowledge } from '../api/subdomainApi';
import StarterKit from '@tiptap/starter-kit';

const props = defineProps<{
  knowledge?: SubdomainKnowledge;
  subdomainId?: string;
}>();

const editable = ref(false);

const title = ref(props.knowledge?.title.toString() || '');

const initialValue = computed(() => props.knowledge?.content || "put your description here..");

const toast = useToast();

const editor = useEditor({
  content: initialValue.value,
  extensions: [
    StarterKit
  ],
  editorProps: {
    attributes: {
      class: 'focus:outline-none'
    }
  },
  editable: editable.value
});

watch(() => editable.value, (val) => editor.value?.setEditable(val))

function edit() {
  editable.value = true;
}

function cancel() {
  editable.value = false;
  editor.value?.commands.setContent(initialValue.value);
  title.value = props.knowledge?.title || '';
}

const isSaving = ref(false);

async function save() {
  if (props.knowledge && editor.value && props.subdomainId) {
    try {
      isSaving.value = true;
      await UpdateSubdomainKnowledge({
        subdomainKnowledgeId: props.knowledge.id || '',
        title: title.value || '',
        content: editor.value.getHTML() || '',
        subdomainId: props.subdomainId
      });
      isSaving.value = false;

      toast.add({
        title: 'Success',
        description: 'Successfully updated knowledge'
      });

      editable.value = false;
    } catch (e: any) {
      isSaving.value = false;
      if ('data' in e) {
        toast.add({
          title: 'Error',
          description: e.data.errorDescription || 'Something went wrong, please try again later',
          color: 'red'
        });
      }
    }

  }
  else {
    toast.add({
      title: 'Error',
      color: 'red',
      description: 'Something went wrong, please refresh the page'
    });
  }
}

</script>

<template>
  <div v-if="knowledge">
    <div class="flex flex-row gap-x-2">
      <p class="text-bold text-2xl" v-if="!editable">{{ knowledge.title }}</p>
      <UInput v-model="title" v-else />
      <UButton 
        size="sm"
        icon="heroicons:pencil"
        variant="ghost"
        color="white"
        @click="edit"
        square
        v-if="!editable"
      />
      <UButton 
        size="sm"
        icon="heroicons:x-mark-16-solid"
        color="red"
        @click="cancel"
        square
        v-if="editable"
      />
      <UButton 
        size="sm"
        icon="heroicons:document-check-16-solid"
        @click="save"
        square
        v-if="editable"
        :loading="isSaving"
      />
    </div>
    <div 
      :class="editable ? 'bg-gray-800 px-3 py-2 rounded-lg mt-2' : ''"
    >
      <EditorContent
        :editor="editor" 
      />
    </div>
  </div>
  <div v-else class="flex flex-row h-full justify-center items-center">
    <p class="text-bold text-xl"></p>
  </div>
</template>
