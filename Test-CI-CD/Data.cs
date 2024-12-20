﻿using Microsoft.EntityFrameworkCore;

namespace Test_CI_CD
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";

    }

    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        

        
    }
}
