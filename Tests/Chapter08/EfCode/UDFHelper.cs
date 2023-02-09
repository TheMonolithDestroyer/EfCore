using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter08.EfCode
{
    public static class UDFHelper
    {
        public const string UDFAverageVotes = nameof(UDFMethods.AveragVotes); //#A

        public static void AddUDFToDatabase(this DbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.ExecuteSqlRaw(
                        $"IF OBJECT_ID('dbo.{UDFAverageVotes}', N'FN') IS NOT NULL " +
                        $"DROP FUNCTION dbo.{UDFAverageVotes}"
                        );

                    context.Database.ExecuteSqlRaw( //#B
                        $"CREATE FUNCTION {UDFAverageVotes} (@bookId int)" + //#C
                        @"  RETURNS float
                          AS
                          BEGIN
                          DECLARE @result AS float
                          SELECT @result = AVG(CAST([NumStars] AS float)) 
                               FROM dbo.Review AS r
                               WHERE @bookId = r.BookId
                          RETURN @result
                          END");
                }
                catch(Exception ex)
                {
                    throw;
                }
            }

            // #A I capture the name of the static method that represents my UDF and use it
            //      as the name of my UDF I add to the database

            // #B I use EF Core's ExecuteSqlCommand method to add the UDF into the database

            // #C The SQL code that follows adds a UDF to a SQL server database
        }
    }
}
