using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.Linq;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Services.DatabaseServices.Concrete
{
    public enum DbStartupModes { UseExisting, EnsureCreated, EnsureDeletedCreated, UseMigrations }

    public static class SetupHelpers
    {
        public static string FormDatabaseConnection(this string connectionString)
        {
            if (connectionString == null)
                throw new InvalidOperationException("You must set the default connection string in the appsetting file.");
            
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            return builder.ToString();
        }

        public static void DevelopmentEnsureCreated(this AppDbContext db)
        {
            db.Database.Migrate();
        }

        public static int SeedDatabase(this AppDbContext context)
        {
            var dbcontext = context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (!dbcontext.Exists())
                throw new InvalidOperationException("The database does not exist. If you are using Migrations then run PMC command update-database to create it");

            var numBooks = context.Books.Count();
            if (numBooks == 0)
            {
                var books = BookJsonLoader.LoadBooks("EFCoreDataSeeding.json").ToList();
                
                context.Books.AddRange(books);
                context.SaveChanges();
                
                context.Books.Add(SpecialBook.CreateSpecialBook());
                context.SaveChanges();
                
                numBooks = books.Count + 1;
            }

            return numBooks;
        }
    }
}
