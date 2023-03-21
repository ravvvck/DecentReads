using DecentReads.Domain.Books;
using GameHost.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Books.Queries.SearchByTitleOrAuthor
{
    public class SearchByTitleOrAuthorQuery : IRequest<List<Book>>
    {
        public string SearchPhrase { get; set; }
    }

    public class SearchByTitleOrAuthorQueryHandler : IRequestHandler<SearchByTitleOrAuthorQuery, List<Book>>
    {
        private readonly IBookRepository bookRepository;

        public SearchByTitleOrAuthorQueryHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public async Task<List<Book>> Handle(SearchByTitleOrAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await bookRepository.SearchByTitleOrAuthorAsync(request.SearchPhrase);
            return result;
        }
    }
}
