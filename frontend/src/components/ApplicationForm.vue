<template>
    <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
      <div class="max-w-md w-full space-y-8">
        <div class="text-center text-2xl font-semibold mb-4"> {{ title }}</div> 
        <form class="space-y-6">
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <label for="Description">Description</label>
              <input
                id="Description"
                name="Description"
                v-model="description"
                placeholder=""
              />
            </div>
            <div class="form-group">
            <label for="date">Resolution Date</label>
            <input type="date" id="date" v-model="resDate" required>
          </div>
          <div class="form-group">
            <label for="time">Resolution Time</label>
            <input type="time" id="time" v-model="resTime" required>
          </div>
          </div>
          <div>
            <button
            @click.prevent="submitForm"
              class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-orange-400 hover:bg-orange-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              <span class="absolute left-0 inset-y-0 flex items-center pl-3">
              </span>
              Add application
            </button>
          </div>
        </form>
        </div>
      </div>
    </template>
    
    <script setup lang="ts">
    import { useApplicationsStore } from '@/stores/applicationsStore';
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';

    defineProps<{ title: String }>();

    const { addApplication } = useApplicationsStore();
    const router = useRouter();

    const description = ref('');
    const resDate = ref('');
    const resTime = ref('');
    
    const submitForm = () => {
      
      const resolutionDateTimeString = `${resDate.value}T${resTime.value}:00.000Z`;
      const dateNow = new Date();
      const entDate = dateNow.toISOString().split('T')[0]; 
      const entTime = dateNow.toTimeString().split(' ')[0]; 
      const formattedEntryDate = `${entDate}T${entTime}.000Z`;

      const application = {
        id: 0,
        description: description.value,
        entryDate: formattedEntryDate,
        resolutionDate: resolutionDateTimeString,
        isSolved: false
      }
      
      console.log(application)

      addApplication(application);
      router.push({ name: 'Applications' });
    };
    </script>