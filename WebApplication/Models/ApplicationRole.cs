using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
namespace WebApplication.Models
{
        [CollectionName("Roles")]
        public class ApplicationRole : MongoIdentityRole<Guid>
        {

        }
    
}
