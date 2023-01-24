using System;
using Microsoft.EntityFrameworkCore;

namespace Tests.Chapter07.EfCode
{
    public class Chapter07DbContext : DbContext
    {
        public Chapter07DbContext(DbContextOptions<Chapter07DbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}