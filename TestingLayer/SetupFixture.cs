using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace TestingLayer
{
    [SetUpFixture]
    public class SetupFixture
    {
        public static GstoreDbContext dbContext;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            DbContextOptionsBuilder builder = new();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new GstoreDbContext(builder.Options);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            dbContext.Dispose();
        }
    }
}
