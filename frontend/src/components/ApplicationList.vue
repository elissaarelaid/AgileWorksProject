<template>
      <div class="titleclass"> {{ title }}</div> 
        <div class="card">
          <div class="table">
            <div class="card-body">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Application Description</th>
                            <th>Entry Date</th>
                            <th>Resolution Date</th>
                        </tr>
                    </thead>
                    <tbody>
                      <tr v-for="application in applications" :key="application.id" :class="getRowClass(application)"> 
                            <td>{{ application.description }}</td>
                            <td>{{ formatEntryDate(application.entryDate) }}</td>
                            <td>{{ formatResolutionDate(application.resolutionDate) }}</td>
                            <td>
                              <button class="button" @click="updateApplicationStatus(application)">
                                Solve</button>
                            </td> 
                          </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useApplicationsStore } from '@/stores/applicationsStore';
import { onMounted } from 'vue';
import moment from 'moment-timezone'
import { Application } from '@/modules/application';
import { storeToRefs } from 'pinia';
const applicationsStore = useApplicationsStore();
const { applications } = storeToRefs(applicationsStore);

defineProps<{ title: String }>();

function formatResolutionDate(value: any) {
    if (value) {
      console.log(value);
      return moment.utc(String(value)).format('MM/DD/YYYY HH:mm');
    }
    return "";
}


function formatEntryDate(value: any) {
    if (value) {
      return moment(String(value)).format('MM/DD/YYYY HH:mm');
    }
    return "";
}

function addHours(date: Date, hours: number): Date {
  const hoursToAdd = hours * 60 * 60 * 1000;
  date.setTime(date.getTime() + hoursToAdd);
  return date;
}

function parseDateStringToDate(value: string | undefined): Date {
    if (!value) {
        throw new Error("Invalid date string: cannot be undefined or empty");
    }
    return new Date(value);
}

function getRowClass(application : Application): string { //make red if is only 1 hour left until resolution date or resolution date is past
  const currentDate = new Date();
  return addHours(parseDateStringToDate(formatResolutionDate(application.resolutionDate)), -1) <= parseDateStringToDate(formatEntryDate(currentDate)) ? 'bg-red-300' : 'bg-white';
}

const updateApplicationStatus = async (application: Application) => {
   const updatedApplication = await applicationsStore.changeApplicationStatus(application.id);
   if (updatedApplication) {
      await applicationsStore.load();
   }
};

onMounted(async () => {
    await applicationsStore.load();
});

</script>

<style>
  .titleclass {
    font-size: 1.7rem; 
    font-weight: bold;
    color: #333; 
    padding-bottom: 10px;
    margin-bottom: 20px; 
    padding-top: 3%;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
  }

  .card {
    background-color: #fff; 
    border-radius: 8px; 
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
    padding: 20px; 
    margin: 20px; 
    border-top: 2px solid #eff1ef; 
}

  .table{
    width: 100%;
    border-collapse: collapse; 
    table-layout: fixed; 
  }

  .table th,
  .table td {
    text-align: left;
    padding: 12px 15px;
    border-bottom: 1px solid #b8b8b8ac;
  }

  .table th {
    background-color: #d3d2d2ad;
    color: #333; 
    font-weight: 650; 
    box-shadow: 0 2px 5px rgba(69, 69, 69, 0.1); 
  }


  .bg-white {
    background-color: #ffffff;
  }

  .bg-red-300 {
    background-color: #e68d8dca; 
  }


  .bg-white:hover {
    background-color: #e1dfdfde; 
  }

  .bg-red-300:hover {
    background-color: #b96969ba; 
  }


    .table tbody tr:last-child td {
      border-bottom: none; 
  }

    .button {
      background-color: #336137;
      color: #ffffff;
      padding: 5px 8px;
      text-align: center;
      font-size: 15px;
      margin-left: 30px;
      border: #101010;
      border-radius: 8px;
      transition: background-color 0.3s, color 0.3s, border-color 0.3s; /* Smooth transition for hover effects */
  }

  .button:hover {
    background-color: #284528; 
    color: #ffffff; 
    border-color: #f5f4f4;
  }

    @media (max-width: 600px) {
      .card {
          margin: 10px;
      }
      .table th, .table td {
          padding: 10px; 
      }
  }
</style>