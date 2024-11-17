using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Queries.CarQueries;
using UdemyCarBook.Application.Features.CQRS.Results.CarResult;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly IRepository<Car> _carRepository;

        public GetCarByIdQueryHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery byIdQuery)
        {
            var values = await _carRepository.GetByIdAsync(byIdQuery.Id);

            return new GetCarByIdQueryResult
            {
                BigImageUrl = values.BigImageUrl,
                BrandID = values.BrandID,
                CarID = values.CarID,
                CoverImageUrl = values.CoverImageUrl,
                Fuel = values.Fuel,
                Km = values.Km,
                Luggage = values.Luggage,
                Model = values.Model,
                Seat = values.Seat,
                Transmission = values.Transmission,
            };

        }
    }
}
