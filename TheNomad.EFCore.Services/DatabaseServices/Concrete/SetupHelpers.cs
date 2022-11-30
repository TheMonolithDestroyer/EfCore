using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System;
using System.IO;
using System.Linq;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Services.DatabaseServices.Concrete
{
    public enum DbStartupModes { UseExisting, EnsureCreated, EnsureDeletedCreated, UseMigrations }

    public static class SetupHelpers
    {
        private const string SeedDataSearchName = "EFCoreDataSeeding.json";
        public const string SeedFileSubDirectory = "SeedData";

        /// <summary>
        /// This forms the connection string with a database name that includes the git branch name
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="gitBranchName"></param>
        /// <returns>returns a connection string with the database name changed by appending that gitBranchName</returns>
        public static string FormDatabaseConnection(this string connectionString)
        {
            if (connectionString == null)
                throw new InvalidOperationException("You must set the default connection string in the appsetting file.");
            
            //In development mode, so we make a new database for each branch, as they could be different
            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            return builder.ToString();
        }

        public static void DevelopmentEnsureCreated(this AppDbContext db)
        {
            db.Database.EnsureCreated();
        }

        public static int SeedDatabase(this AppDbContext context)
        {
            var dbcontext = context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (!dbcontext.Exists())
                throw new InvalidOperationException("The database does not exist. If you are using Migrations then run PMC command update-database to create it");

            var numBooks = context.Books.Count();
            if (numBooks == 0)
            {
                //the database is emply so we fill it from a json file
                var books = BookJsonLoader.LoadBooks(SeedDataSearchName).ToList();
                context.Books.AddRange(books);
                context.SaveChanges();
                //We add this separately so that it has the highest Id. That will make it appear at the top of the default list
                context.Books.Add(SpecialBook.CreateSpecialBook());
                context.SaveChanges();
                numBooks = books.Count + 1;
            }

            return numBooks;
        }
    }
}
