﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.DatabaseServices.Concrete
{
    public static class BookJsonLoader
    {
        public static IEnumerable<Book> LoadBooks(string fileSearchString)
        {
            var fileDir = @"C:\Users\khozhakeldiyev_n\source\repos\TheNomad.EFCore\SeedData";
            var filePath = GetJsonFilePath(fileDir, fileSearchString);
            var jsonDecoded = JsonConvert.DeserializeObject<ICollection<BookInfoJson>>(File.ReadAllText(filePath));

            var authorDict = new Dictionary<string, Author>();
            foreach (var bookInfoJson in jsonDecoded)
            {
                foreach (var author in bookInfoJson.authors)
                {
                    if (!authorDict.ContainsKey(author))
                        authorDict[author] = new Author { Name = author };
                }
            }

            return jsonDecoded.Select(x => CreateBookWithRefs(x, authorDict));
        }

        private static Book CreateBookWithRefs(BookInfoJson bookInfoJson, Dictionary<string, Author> authorDict)
        {
            var book = new Book
            {
                Title = bookInfoJson.title,
                Description = bookInfoJson.description,
                PublishedOn = DecodePubishDate(bookInfoJson.publishedDate),
                Publisher = bookInfoJson.publisher,
                Price = (decimal)(bookInfoJson.saleInfoListPriceAmount ?? -1),
                ImageUrl = bookInfoJson.imageLinksThumbnail
            };

            byte i = 0;
            book.AuthorsLink = new List<BookAuthor>();
            foreach (var author in bookInfoJson.authors)
            {
                book.AuthorsLink.Add(new BookAuthor { Book = book, Author = authorDict[author], Order = i++ });
            }

            if (bookInfoJson.averageRating != null)
                book.Reviews = CalculateReviewsToMatch((double)bookInfoJson.averageRating, (int)bookInfoJson.ratingsCount);

            return book;
        }

        internal static ICollection<Review> CalculateReviewsToMatch(double averageRating, int ratingsCount)
        {
            var reviews = new List<Review>();
            var currentAve = averageRating;
            for (int i = 0; i < ratingsCount; i++)
            {
                reviews.Add(new Review
                {
                    VoterName = "anonymous",
                    NumStars = (int)(currentAve > averageRating ? Math.Truncate(averageRating) : Math.Ceiling(averageRating))
                });
                currentAve = reviews.Average(x => x.NumStars);
            }
            return reviews;
        }

        private static DateTime DecodePubishDate(string publishedDate)
        {
            var split = publishedDate.Split('-');
            switch (split.Length)
            {
                case 1:
                    return new DateTime(int.Parse(split[0]), 1, 1);
                case 2:
                    return new DateTime(int.Parse(split[0]), int.Parse(split[1]), 1);
                case 3:
                    return new DateTime(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            }

            throw new InvalidOperationException($"The json publishedDate failed to decode: string was {publishedDate}");
        }

        private static string GetJsonFilePath(string fileDir, string searchPattern)
        {
            var fileList = Directory.GetFiles(fileDir, searchPattern);

            if (fileList.Length == 0)
                throw new FileNotFoundException($"Could not find a file with the search name of {searchPattern} in directory {fileDir}");

            //If there are many then we take the most recent
            return fileList.ToList().OrderBy(x => x).Last();
        }
    }
}
