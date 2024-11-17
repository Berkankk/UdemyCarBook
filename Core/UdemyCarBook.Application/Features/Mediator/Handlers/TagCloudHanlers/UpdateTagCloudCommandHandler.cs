using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.TagCloudHanlers
{
    public class UpdateTagCloudCommandHandler : IRequestHandler<UpdateTagCloudCommand>
    {
        private readonly IRepository<TagCloud> _tagCloudRepository;

        public UpdateTagCloudCommandHandler(IRepository<TagCloud> tagCloudRepository)
        {
            _tagCloudRepository = tagCloudRepository;
        }

        public async Task Handle(UpdateTagCloudCommand request, CancellationToken cancellationToken)
        {
            var value = await _tagCloudRepository.GetByIdAsync(request.TagCloudID);
            value.BlogID = request.BlogID;
            value.Title = request.Title;
            value.TagCloudID = request.TagCloudID;
            await _tagCloudRepository.UpdateAsync(value);
        }
    }
}
