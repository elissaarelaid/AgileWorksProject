<template>
    <nav>
      <router-link
                to="/"
                class="logo hover:bg-gray-700 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
                active-class="bg-black"
                >ApplicationApp</router-link>
              <router-link
                to="/add"
                class="text hover:bg-gray-700 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
                active-class="bg-black"
                >Add new application</router-link>
    </nav>
  </template>
  
  <script>
  export default {
    name: 'Navbar'
  }
  </script>
  
  <style scoped>
  nav {
    background-color: #333; 
    color: #fff; 
    box-shadow: 0 2px 5px rgba(0,0,0,0.2); 
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    display: flex; 
    justify-content: space-between; 
    padding: 0 20px; 
    align-items: center; 
    height: 60px; 
    z-index: 1000;
  }
  
  .logo {
    font-size: 24px; 
    font-weight: bold; 
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    color:azure
  }

  .text{
    font-size: 13px; 
    font-weight: bold;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    color:azure
  }

  </style>
  