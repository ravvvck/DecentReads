using DecentReads.Domain.Authors;
using DecentReads.Domain.Books.Entities;
using DecentReads.Domain.Common;
using DecentReads.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Books
{
    public sealed class Book : BaseAuditableEntity
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Author Author { get; private set; }
        public string Genres { get; private set; }
        public string Image_URL { get; private set; }
        public string ISBN { get; private set; }
        public int NumberOfPages { get; private set; }
        public int Publication_day { get; private set; }
        public int Publication_month { get; private set; }
        public int Publication_year { get; private set; }

        private readonly List<Rating>? _ratings = new();
        public List<Rating>? Ratings => _ratings;



        public static Book Create(string name, string description, Author author, string genres, string image_URL,
            string iSBN, int numberOfPages, int publication_day,
            int publication_month, int publication_year)
        {
            return new(name, description, author, genres, image_URL,
             iSBN, numberOfPages, publication_day,
             publication_month, publication_year);
        }




        private Book()
        {

        }

        private Book(string name, string description, Author author, string genres, string image_URL,
            string iSBN, int numberOfPages, int publication_day,
            int publication_month, int publication_year)
        {
            Name = name;
            Description = description;
            Author = author;
            Genres = genres;
            Image_URL = image_URL;
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            Publication_day = publication_day;
            Publication_month = publication_month;
            Publication_year = publication_year;
        }
    }
}