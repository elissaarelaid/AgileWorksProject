<template>
    <ApplicationForm :title="title"/>
   </template>
   
   <script setup lang="ts">
   import ApplicationForm from '@/components/ApplicationForm.vue';
   
   defineProps<{ title: String }>();
   </script>