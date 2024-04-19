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
                            <th>ID</th>
                            <th>Application Description</th>
                            <th>Entry Date</th>
                            <th>Resolution Date</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(application, index) in applications" :key="index">
                            <td>{{ application.id }}</td>
                            <td>{{ application.description }}</td>
                            <td>{{ application.entryDate }}</td>
                            <td>{{ application.resolutionDate }}</td>
                            <td>{{ application.isSolved ? 'Solved' : 'Pending' }}</td>
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
defineProps<{ title: String }>();


const { applications, loadApplications } = useApplicationsStore();

onMounted(async () => {
    await loadApplications();
   //console.log("Data in component:", applications.value); 
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

  @media (max-width: 600px) {
    .card {
        margin: 10px;
    }
    .table th, .table td {
        padding: 10px; 
    }
}
</style>