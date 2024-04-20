import { defineStore } from 'pinia';
import { ref, Ref } from 'vue';
import useApi from '@/modules/api';
import { Application } from '@/modules/application';

export const useApplicationsStore = defineStore('applicationsStore', () => {
    const applications: Ref<Application[] | undefined> = ref();
    
      let allApplications: Application[] = [];

      const loadApplications = async () => {
        const apiGetApplications = useApi<Application[]>('Applications');
    
        await apiGetApplications.request();
    
        if (apiGetApplications.response.value) {
          return apiGetApplications.response.value!;
        }
    
        return [];
      };
    
      const load = async () => {
        allApplications = await loadApplications();
        applications.value = allApplications;
      };


    const changeApplicationStatus = async (id: number): Promise<Application | null> => {
      const apiUpdateApplicationStatus = useApi<Application>(`Applications/${id}`);
      const options = {
        method: 'PUT'
    };
      await apiUpdateApplicationStatus.request(options);
    
      if (apiUpdateApplicationStatus.response.value) {
        return apiUpdateApplicationStatus.response.value;
      } 

      return null;
    };

    const addApplication = async (application: Application) => {
      const apiAddApplication = useApi<Application>('Applications');
      const options = {
        method: 'POST',
        body: JSON.stringify(application),
        headers: {
          'Content-Type': 'application/json' 
        },
      };
      
      await apiAddApplication.request(options);

      if (apiAddApplication.response.value) {
        allApplications.push(apiAddApplication.response.value!);
        applications.value = allApplications;
      }
    };


    return { applications, load, changeApplicationStatus, addApplication };
});
