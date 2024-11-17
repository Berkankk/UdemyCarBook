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
    public class CreateCarCommadHandler
    {
        private readonly IRepository<Car>  _carRepository;

        public CreateCarCommadHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(CreateCarCommand command)
        {
            await _carRepository.CreateAsync(new Car
            {
                BigImageUrl = command.BigImageUrl,
                BrandID = command.BrandID,
                Model = command.Model,
                CoverImageUrl = command.CoverImageUrl,
                Km = command.Km,
                Transmission = command.Transmission,
                Seat = command.Seat,
                Luggage = command.Luggage,
                Fuel = command.Fuel,

            });
        }
    }
}
