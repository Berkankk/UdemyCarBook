using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly IRepository<Blog> _blogRepository;

        public UpdateBlogCommandHandler(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var value = await _blogRepository.GetByIdAsync(request.BlogID);
            value.CategoryID = request.CategoryID;
            value.CreatedDate = request.CreatedDate;
            value.CoverImageUrl = request.CoverImageUrl;
            value.AuthorID = request.AuthorID;
            value.Title = request.Title;
            value.BlogID = request.BlogID;
            await _blogRepository.UpdateAsync(value);
        }
    }
}
