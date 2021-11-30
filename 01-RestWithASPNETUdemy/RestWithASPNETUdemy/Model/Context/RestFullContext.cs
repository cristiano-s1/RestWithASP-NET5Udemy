﻿using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class RestFullContext : DbContext
    {
        public RestFullContext()
        { }

        public RestFullContext(DbContextOptions<RestFullContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }   
    }
}