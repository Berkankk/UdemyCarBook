using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.ContactCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class UpdateContactCommandHandler
    {
        private readonly IRepository<Contact> _contactRepository;

        public UpdateContactCommandHandler(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task Handle(UpdateContactCommand command)
        {
            var value = await _contactRepository.GetByIdAsync(command.ContactID);
            value.Email = command.Email;
            value.Name = command.Name;
            value.Subject = command.Subject;
            value.Message = command.Message;
            value.SendDate = command.SendDate;
            await _contactRepository.UpdateAsync(value);
        }
    }
}
