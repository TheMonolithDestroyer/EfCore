using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNomad.EFCore.Data
{
    public class ContextFactoryNeededForMigrations : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// This class is needed to allow Add-Migrations command to be run. 
        /// It is not a good implmentation as it has to have a constant connection sting in it
        /// but it is Ok on a local machine, which is where you want to run the command
        /// </summary>
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(
                @"Server=localhost; port=5432; user id=postgres; password=pdMsWFjZ; database=EFCore; pooling=true;SearchPath=public", 
                b => b.MigrationsAssembly("TheNomad.EFCore.Data"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
