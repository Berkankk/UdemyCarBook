using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.CarResult;
using UdemyCarBook.Application.Interfaces.CarInterfaces;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetLast5CarsWithBrandQueryHandler
    {
        private readonly ICarRepository _carRepository;

        public GetLast5CarsWithBrandQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<GetLast5CarsWithBrandQueryResult> Handle()
        {
            var value = _carRepository.GetLast5CarsWithBrands();
            return value.Select(x => new GetLast5CarsWithBrandQueryResult
            {
                BrandName=x.Brand.Name,
                BrandID = x.BrandID,
                BigImageUrl=x.BigImageUrl,
                CarID=x.CarID,
                CoverImageUrl=x.CoverImageUrl,
                Fuel=x.Fuel,
                Km=x.Km,
                Luggage=x.Luggage,
                Model=x.Model,
                Seat=x.Seat,
                Transmission=x.Transmission,
            }).ToList();
        }
    }
}
