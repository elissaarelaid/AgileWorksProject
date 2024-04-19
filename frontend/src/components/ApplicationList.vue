<template>
    <div class="card">
        <div class="table">
            <div class="title">
                <h4 >{{ title }}</h4>    
            </div>
            <div class="card-body">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <!-- <th>ID</th> -->
                            <th>Application Description</th>
                            <th>Entry Date</th>
                            <th>Resolution Date</th>
                            <!-- <th>Status</th> -->
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(application, index) in applications" :key="index">
                            <!-- <td>{{ application.id }}</td> -->
                            <td>{{ application.description }}</td>
                            <td>{{ formatDate(application.entryDate) }}</td>
                            <td>{{ formatDate(application.resolutionDate) }}</td>
                            <!-- <td>{{ application.isSolved ? 'Solved' : 'Pending' }}</td> -->
                            <td>
                              <button class="button" @click="updateApplicationStatus(application)">
                                Solve</button>
                            </td> <!-- Place the button in a new column -->
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
import moment from 'moment'
import { Application } from '@/modules/application';
import { storeToRefs } from 'pinia';
const applicationsStore = useApplicationsStore();
const { applications } = storeToRefs(applicationsStore);

function formatDate(value: any) {
    if (value) {
        return moment(String(value)).format('MM/DD/YYYY hh:mm')
    }
}


defineProps<{ title: String }>();

const updateApplicationStatus = async (application: Application) => {
   const updatedApplication = await applicationsStore.changeApplicationStatus(application.id);
   if (updatedApplication) {
      applicationsStore.load();
   }
};

onMounted(async () => {
    applicationsStore.load();
});

</script>

<style>
  .title {
    font-size: 1.5rem; /* Larger font size for logo */
    font-weight: bold; /* Bold font for emphasis */
    color: #333; /* Dark gray for text for better readability */
    padding-bottom: 20px; /* Space below the logo/header */
    margin-bottom: 20px; 
  }

  .card {
    background-color: #fff; /* White background */
    border-radius: 8px; /* Rounded corners */
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);/* Subtle shadow for depth */
    padding: 20px; /* Padding inside the card */
    margin: 20px; /* Margin around the card */
}

  .table{
    width: 100%;
    border-collapse: collapse; 
    table-layout: fixed; /*equal column width*/
  }

  .table th,
  .table td {
    text-align: left;
    padding: 12px 15px;
    border-bottom: 1px solid #b8b8b8ac;
  }

  .table th {
    background-color: #d3d2d2ad;
    color: #333; /* Dark text for contrast */
    font-weight: 650; 
    box-shadow: 0 2px 5px rgba(69, 69, 69, 0.1); 
  }

  .table tr:hover {
    background-color: #c5c5c536; /*hover color*/ 
  }

  .table tbody tr:last-child td {
    border-bottom: none; /* No border for the last row */
}

  .button {
    background-color: #336137;
    color: #ffffff;
    padding: 5px 8px;
    text-align: center;
    font-size: 15px;
    margin-left: 30px;
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