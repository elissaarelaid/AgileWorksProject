<template>
    <div>
      <div>
        <div class="title"> {{ title }}</div> 
        <form class="formcard">
          <div>
            <div>
              <label class="text" for="Description">Description </label>
              <textarea id="Description" name="Description"
                v-model="description" placeholder="" />
            </div>
            <div class="form-group">
            <label class="text" for="date">Resolution Date </label>
            <input type="date" id="date" v-model="resDate" required>
          </div>
          <div class="form-group">
            <label class="text" for="time">Resolution Time </label>
            <input type="time" id="time" v-model="resTime" required>
          </div>
          </div>
          <div>
            <button class="formbutton"
            @click.prevent="submitForm">
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

<style>
  .title {
    font-size: 1.5rem;
    font-weight: bold; 
    color: #333; 
    padding-bottom: 10px;
    margin-bottom: 20px; 
    padding-top: 5%;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
  }

  
  .formcard {
    background-color: #d7e2d7; 
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0,0,0,0.2);
    padding: 20px; 
    margin: 20px; 
    margin-bottom: 5px;
    width: 500px
}

  .form-group {
    margin-bottom: 20px;
  }

  .text {
    color:#364a38;
    font-weight: bold; 
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    display: block;
    text-align: left;
  }

  input[type="date"],
  input[type="time"],
  textarea {
    width: 100%;
    padding: 10px;
    box-sizing: border-box;
    border: 1px solid #ccc;
    border-radius: 4px;
    resize: none;
  }

  .formbutton {
    background-color: #364a38;
    color: #f5f4f4;
    padding: 5px 100px;
    text-align: center;
    font-size: 15px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
    border: 2px solid #898b89; /* Dark green border */
    border-radius: 8px; 
    transition: background-color 0.3s, color 0.3s, border-color 0.3s; /* Smooth transition for hover effects */
}

.formbutton:hover {
  background-color: #2c382c; 
  color: #ffffff; 
  border-color: #f5f4f4;
}

  textarea {
  width: 100%;
  padding: 10px; 
  box-sizing: border-box;
  border: 1px solid #ccc; 
  border-radius: 4px; 
  resize: vertical; 
  background-color: #fff; 
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
}


/* @media (max-width: 600px) {
    .formcard {
        margin: 10px;
    }
    .table th, .table td {
        padding: 10px; 
    }
  } */
</style>