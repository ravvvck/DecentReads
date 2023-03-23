using DecentReads.Domain.Common;
using DecentReads.Domain.Common.Models;
using DecentReads.Domain.Exceptions;
using DecentReads.Domain.Users.Entities;
using DecentReads.Domain.Users.Enums;
using DecentReads.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Domain.Users
{
    public  class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.User;
        public DateTime RefreshTokenCreated { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        private readonly List<FavoriteBook> _favoriteBooks = new();
        public List<FavoriteBook> FavoriteBooks => _favoriteBooks;


        private User()
        {

        }

        private User (string username, string email, string password)
        {
           Username = username;
            Email = email;
            PasswordHash = password;
            
        }

        public static User Create(string username, string email, string password)
        {
            return new(username,email, password);
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            this.RefreshToken = refreshToken.Token;
            this.RefreshTokenCreated = refreshToken.Created;
            this.RefreshTokenExpires = refreshToken.Expires;
        }

        public void AddBookToFavorites(int bookId)
        {
            var alreadyExist = this.FavoriteBooks.Any(p => p.BookId == bookId);
            if (alreadyExist)
            {
                throw new UserHasAlreadyAddedThisBookToFavoritesException("User has already added this book to favorites.");
            }
            var bookToAdd = FavoriteBook.Create(bookId, DateTime.Now);
            this.FavoriteBooks.Add(bookToAdd);
        }

        public void DeleteBookFromFavorites(int bookId)
        {
            var alreadyExist = this.FavoriteBooks.FirstOrDefault(p => p.BookId == bookId);
            if (alreadyExist == null)
            {
                throw new BookIsNotInFavoritesException("Book is not in the user's favorites.");
            }
            this.FavoriteBooks.Remove(alreadyExist); 
        }
    }
}
