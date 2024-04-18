<script setup lang="ts">
import { useEditor, EditorContent } from '@tiptap/vue-3';
import type { SubdomainKnowledge } from '../api/subdomainApi';
import StarterKit from '@tiptap/starter-kit';

const props = defineProps<{
  knowledge?: SubdomainKnowledge
}>();

const editor = useEditor({
  content: props.knowledge?.content || "put your description here..",
  extensions: [
    StarterKit
  ],
  editorProps: {
    attributes: {
      class: 'focus:outline-none'
    }
  }
});

</script>

<template>
  <div v-if="knowledge">
    <p class="text-bold text-xl">{{ knowledge.title }}</p>
    <EditorContent 
      :editor="editor" 
    />
  </div>
  <div v-else class="flex flex-row h-full justify-center items-center">
    <p class="text-bold text-xl"></p>
  </div>
</template>
