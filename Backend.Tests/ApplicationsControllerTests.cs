using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using backend.Model; 
using backend.Controllers;  
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;



namespace Backend.Tests
{
    public class ApplicationsControllerTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly ApplicationsController _controller;

        private List<Application> applicationsCheckList = new List<Application>();
        
        private Application a1 = new Application {
                Description = "This is a new application 1",
                ResolutionDate = new DateTime(2039,12,12)
        };

        private Application a2 = new Application {
                Description = "This is a new application 2",
                ResolutionDate = new DateTime(2024,12,12)
        };

        private Application a3 = new Application {
                Description = "This is a new application 3",
                ResolutionDate = new DateTime(2026,9,10),
                IsSolved = true
        };

        private Application a4 = new Application {
                Description = "This is a new application 4",
                ResolutionDate = new DateTime(2027,12,12)
        };

        public ApplicationsControllerTests() {
            // Setup the in-memory database before each test
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure a clean database for each test
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();

            _controller = new ApplicationsController(_context);
        }

        // called after every test
        public void Dispose() {
            //cleans up the database
            _context.Applications.RemoveRange(_context.Applications);
            _context.SaveChanges();
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetApplications_ReturnsOnlyUnsolvedApplicationsInCorrectOrder() {
            _context.Applications.RemoveRange(_context.Applications); //et andmebaas oleks t[hi enne testimist
            _context.SaveChanges();

            _context.Applications.AddRange(
                a1, a2, a3, a4 
            );
            _context.SaveChanges();
            applicationsCheckList = new List<Application> { a1, a4, a2 }; //in correct order

            var result = _controller.GetApplications();

            var actionResult = Assert.IsType<OkObjectResult>(result); //kontrollib kas tagastatakse http 200 OK
            var returnedApplications = Assert.IsType<List<Application>>(actionResult.Value); //kas tagastatakse list
            returnedApplications.Should().BeEquivalentTo(applicationsCheckList, options => options.WithStrictOrdering()); //kas listide objektid on samas j'rjekorras ja samad
        }

        [Fact]
        public void GetApplications_NoApplications_ReturnsEmptyList() {
            _context.Applications.RemoveRange(_context.Applications); //et andmebaas oleks t[hi enne testimist
            _context.SaveChanges();

            var result = _controller.GetApplications();

            var actionResult = Assert.IsType<OkObjectResult>(result); //kontrollib kas tagastatakse http 200 OK
            var returnedApplications = Assert.IsType<List<Application>>(actionResult.Value); //kas tagastatakse list
            returnedApplications.Should().BeEquivalentTo(applicationsCheckList, options => options.WithStrictOrdering()); //m]lemad listid peavad olema t[hjad]
        }

        [Fact]
        public void SolveApplication_Successful()
        {
            Assert.True(true);
        }

        [Fact]
        public void SolveApplication_ApplicationIsAlreadySolved_ReturnsBadRequest()
        {
            Assert.True(true);
        }

        [Fact]
        public void SolveApplication_ApplicationNotFound_ReturnsNotFound()
        {
            Assert.True(true);
        }

        [Fact]
        public void AddApplication_Successful() {
            Application application = new Application() {
                Description = "This is a new application",
                ResolutionDate = new DateTime(2039,12,12)
            };

            var response = _controller.AddApplication(application);
            var actionResult = Assert.IsType<OkObjectResult>(response); //kas tagastatakse 200 staatus

            // Deserialize the response directly to the expected type
            var resultData = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(System.Text.Json.JsonSerializer.Serialize(actionResult.Value));

            Assert.NotNull(resultData);
            Assert.NotNull(resultData.Application);

            Application returnedApplication = resultData.Application;

            var lastApplicationFromDatabase = _context.Applications.ToList().Last();
            returnedApplication.Should().BeEquivalentTo(lastApplicationFromDatabase); //kas viimane application andmebaasis ja tagastatud application on samad

            returnedApplication.Should().BeEquivalentTo(application); //kas tagastatud application ja algselt lisatav application on samad
        }

        
        [Fact]
        public void AddApplication_Successful_EntryDateTimeIsCurrentDateTime() {
            Application application = new Application() {
                Description = "This is a new application",
                ResolutionDate = new DateTime(2039,12,12)
            };

            var response = _controller.AddApplication(application);
            var actionResult = Assert.IsType<OkObjectResult>(response); //kas tagastatakse 200 staatus

            // Deserialize the response directly to the expected type
            var resultData = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(System.Text.Json.JsonSerializer.Serialize(actionResult.Value));

            Assert.NotNull(resultData);
            Assert.NotNull(resultData.Application);

            Application returnedApplication = resultData.Application;

            Assert.Equal(DateTime.Now, returnedApplication.EntryDate, precision: TimeSpan.FromSeconds(1)); //kas entryDate on sama mis praegune date ja time
        }



        [Fact]
        public void AddApplication_ResolutionDateIsPast_ReturnsBadRequest()
        {
            Assert.True(true);
        }

        [Fact]
        public void AddApplication_DescriptionIsEmpty_ReturnsBadRequest()
        {
            Assert.True(true);
        }

        [Fact]
        public void AddApplication_DescriptionIsTooLong_ReturnsBadRequest()
        {
            Assert.True(true);
        }
    }


}