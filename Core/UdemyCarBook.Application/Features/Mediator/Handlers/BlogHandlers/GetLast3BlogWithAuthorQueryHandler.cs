using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces.BlogInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GetLast3BlogWithAuthorQueryHandler : IRequestHandler<GetLast3BlogWithAuthorQuery, List<GetLast3BlogWithAuthorQueryResult>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetLast3BlogWithAuthorQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<GetLast3BlogWithAuthorQueryResult>> Handle(GetLast3BlogWithAuthorQuery request, CancellationToken cancellationToken)
        {
            var values = _blogRepository.GetLast3BlogWithAuthors();
            return values.Select(x => new GetLast3BlogWithAuthorQueryResult
            {
                AuthorID = x.AuthorID,
                BlogID = x.BlogID,
                CategoryID = x.CategoryID,
                CoverImageUrl = x.CoverImageUrl,
                CreatedDate = x.CreatedDate,
                Title = x.Title,
                AuthorName = x.Author.Name
                
            }).ToList();

        }
    }
}
