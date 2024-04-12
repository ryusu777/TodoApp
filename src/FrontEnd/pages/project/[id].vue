<script setup lang="ts">
const route = useRoute();
const projectCode = route.params.id;
</script>

<template>
  <h1>{{ projectCode }}</h1>
</template>
