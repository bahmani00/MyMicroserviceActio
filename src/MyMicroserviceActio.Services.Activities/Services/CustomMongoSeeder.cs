using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMicroserviceActio.Common.Mongo;
using MyMicroserviceActio.Services.Activities.Domain.Models;
using MyMicroserviceActio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MyMicroserviceActio.Common.Services;
using System;

namespace MyMicroserviceActio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IActivityRepository activityRepository;

        public CustomMongoSeeder(IMongoDatabase database, 
            ICategoryRepository categoryRepository, 
            IActivityRepository activityRepository) 
            : base(database)
        {
            this.categoryRepository = categoryRepository;
            this.activityRepository = activityRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<Category>
            {
                new Category(Constants.CategoryId_Hobby, "hobby"),
                new Category(Constants.CategoryId_Sport, "sport"),
                new Category(Constants.CategoryId_Work, "work")
            };
            await Task.WhenAll(
                categories.Select(x => categoryRepository.AddAsync(x))
                );

            await activityRepository.AddAsync(
                new Activity(Constants.ActivityId_Admin, 
                new Category(Constants.CategoryId_Hobby, "hobby"), 
                Constants.UserId_Admin,
                "Admin", "Admin user's activity", DateTime.UtcNow)
                );

        }
    }
}