using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.CarResult;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly IRepository<Car> _carRepository;

        public GetCarQueryHandler(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<GetCarQueryResult>> Handle()
        {
            var values = await _carRepository.GetAllAsync();
            return values.Select(x => new GetCarQueryResult
            {
                CarID = x.CarID,
                BigImageUrl = x.BigImageUrl,
                BrandID = x.BrandID,
                CoverImageUrl = x.CoverImageUrl,
                Fuel=x.Fuel,
                Km = x.Km,
                Luggage=x.Luggage,
                Model = x.Model,
                Transmission = x.Transmission,
                Seat = x.Seat
            }).ToList();
        }
    }
}
