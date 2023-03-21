using DecentReads.Application.Common.Interfaces.Persistence;
using DecentReads.Domain.Authors;
using DecentReads.Domain.Books;
using GameHost.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string AuthorFullName { get;  set; }
        public string Genres { get;  set; }
        public string Image_URL { get;  set; }
        public string ISBN { get;  set; }
        public int NumberOfPages { get;  set; }
        public int Publication_day { get;  set; }
        public int Publication_month { get;  set; }
        public int Publication_year { get;  set; }
    }


    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await authorRepository.AddAsync(request.AuthorFullName);
            
            var newBook = Book.Create(
                request.Name,
                request.Description,
                author,
                request.Genres,
                request.Image_URL,
                request.ISBN,
                request.NumberOfPages,
                request.Publication_day,
                request.Publication_month,
                request.Publication_year);

            var result = await bookRepository.CreateAsync(newBook);
            return result;
        }
    }
}
