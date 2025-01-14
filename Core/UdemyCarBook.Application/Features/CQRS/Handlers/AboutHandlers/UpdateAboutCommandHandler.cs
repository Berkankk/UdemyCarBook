﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.AboutCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers
{
    public class UpdateAboutCommandHandler
    {
        private readonly IRepository<About> _aboutRepository;

        public UpdateAboutCommandHandler(IRepository<About> aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task Handle(UpdateAboutCommand command)
        {
            var values = await _aboutRepository.GetByIdAsync(command.AboutID);
            values.Description = command.Description;
            values.ImageUrl = command.ImageUrl;
            values.Title = command.Title;
            await _aboutRepository.UpdateAsync(values);
        }
    }
}
