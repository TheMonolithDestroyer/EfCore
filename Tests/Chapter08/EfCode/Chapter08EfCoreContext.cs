using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter08.EfCode
{
    public class Chapter08EfCoreContext : DbContext
    {
        public Chapter08EfCoreContext(DbContextOptions<Chapter08EfCoreContext> options)
            : base(options) 
        { }

        [DbFunction(Schema = "dbo")] // #A
        public static double? AverageVotes(int id) // #B
        {
            throw new NotImplementedException("Called in Client vs. Server evaliuation."); // #C
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UDF(user defined functions) can be registred here through Fluent API
            //modelBuilder.HasDbFunction( // #D
            //    () => UDFMethods.AveragVotes(default)) // #E
            //    .HasSchema("dbo");
        }

        // #A Defines the method as being a representation of your UDF.
        //      The DbFunction can be used without any parameters, but
        //      here it’s setting the schema because EF Core 2.0 didn’t set the
        //      default schema property(fixed in 2.1).

        // #B The return value, the method name, and the number, type,
        //      and order of the method parameters must match your UDF code.

        // #C If your query that uses the scalar UDF is converted into a
        //      client vs.server evaluation, this software method will be executed
        //      client-side. NotImplementedException will be called if that happens;
        //      you can then decide what you want to do about it.

        // #D Here I implemented UDF with FluentAPI. The .HasDbFunction() registers
        //      my static method as the way to access your UDF.

        // #E Adds a call to your static method representation of your UDF code.
        //      The method isn’t called, but the lambda function is read to find out the name,
        //      return type, and parameters of method.

    }
}
