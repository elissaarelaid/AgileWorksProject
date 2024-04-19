<template>
 <ApplicationList :title="title"/>
</template>

<script setup lang="ts">
import ApplicationList from '@/components/ApplicationList.vue';

defineProps<{ title: String }>();
</script>