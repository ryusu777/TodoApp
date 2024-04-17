<script setup lang="ts">
import { useEditor, EditorContent } from '@tiptap/vue-3';
import type { SubdomainKnowledge } from '../api/subdomainApi';
import StarterKit from '@tiptap/starter-kit';

const props = defineProps<{
  knowledge: SubdomainKnowledge
}>();

const editor = useEditor({
  content: props.knowledge.content,
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
  <div>
    <p class="text-bold text-xl">{{ knowledge.title }}</p>
    <EditorContent 
      :editor="editor" 
    />
  </div>
</template>
