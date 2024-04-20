<template>
    <nav>
      <div class="logo">ApplicationApp</div>
      <ul>
      </ul>
    </nav>
  </template>
  
  <script>
  export default {
    name: 'Navbar'
  }
  </script>
  
  <style scoped>
  nav {
    background-color: #333; /* Dark background for a modern look */
    color: #fff; /* White text color */
    box-shadow: 0 2px 5px rgba(0,0,0,0.2); /* Subtle shadow for depth */
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    display: flex; /* Use flexbox for better alignment */
    justify-content: space-between; /* Space between logo and navigation links */
    padding: 0 20px; /* Padding on both sides */
    align-items: center; /* Align items vertically */
    height: 60px; /* Fixed height */
    z-index: 1000;
  }
  
  .logo {
    font-size: 24px; /* Larger font size for logo */
    font-weight: bold; /* Make logo bold */
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
  }
  
    nav ul {
        list-style-type: none;
        display: flex; /* Display links in a row */
        align-items: center; /* Align links vertically */
        margin: 0; /* Remove default margin */
    }
    
    nav ul li {
        margin: 0 15px; /* Space between links */
    }
    
    router-link {
        color: #fff; /* White text color for links */
        text-decoration: none; /* No underline */
        transition: color 0.3s ease; /* Smooth transition for hover effect */
    }
    
    router-link:hover {
        color: #4CAF50; /* Green color on hover */
    }
  </style>
  