import { defineStore } from 'pinia';
import { ref, Ref } from 'vue';
import useApi from '@/modules/api';
import { Application } from '@/modules/application';

export const useApplicationsStore = defineStore('applicationsStore', () => {
    const applications: Ref<Application[] | undefined> = ref();

    const loadApplications = async () => {
      const { request, response } = useApi<Application[]>('applications');

      try {
        await request(); // This fetches and updates the response ref
        applications.value = response.value; // Use the updated ref
        console.log("Applications loaded:", applications.value);
      } catch (error) {
        console.error("Failed to load applications:", error);
      }
    };

    return { applications, loadApplications };
});
