using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Commands.CarCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class UpdateCarCommandHandler
    {
        private readonly IRepository<Car> _carRepository;

        public UpdateCarCommandHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(UpdateCarCommand command)
        {
            var values = await _carRepository.GetByIdAsync(command.CarID);
            values.Fuel = command.Fuel;
            values.BrandID = command.BrandID;
            values.Model = command.Model;
            values.CoverImageUrl = command.CoverImageUrl;
            values.Km = command.Km;
            values.Transmission = command.Transmission;
            values.Seat = command.Seat;
            values.Luggage = command.Luggage;
            values.BigImageUrl = command.BigImageUrl;
            await _carRepository.UpdateAsync(values);
        }
    }
}