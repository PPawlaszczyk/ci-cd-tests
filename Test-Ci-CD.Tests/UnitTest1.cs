using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test_CI_CD;

namespace Test_Ci_CD.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1==1);
        }
        [Fact]
        public async Task CustomerIntgerationTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();

            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext(optionsBuilder.Options);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            //context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
            //await context.SaveChangesAsync();

            var controller = new CustomersController(context);

            await controller.Add(new Customer(){ CustomerName="Foobar"});

            var result = (await controller.GetAll()).ToArray();

            Assert.Single(result);
            Assert.Equal("Foobar", result[0].CustomerName);
        }
    }
}