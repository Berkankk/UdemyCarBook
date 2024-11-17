using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.CQRS.Commands.CarCommands;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers;
using UdemyCarBook.Application.Features.CQRS.Queries.CarQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly GetCarQueryHandler _queryHandler;
        private readonly GetCarByIdQueryHandler _byIdQueryHandler;
        private readonly CreateCarCommadHandler _createCarCommadHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly RemoveCarCommandHandler _removeCarCommandHandler;
        private readonly GetCarWithBrandqueryHandler _getCarWithBrandqueryHandler;
        private readonly GetLast5CarsWithBrandQueryHandler _getLast5CarsWithBrandQueryHandler;

        public CarsController(GetCarQueryHandler queryHandler, GetCarByIdQueryHandler byIdQueryHandler, CreateCarCommadHandler createCarCommadHandler, UpdateCarCommandHandler updateCarCommandHandler, RemoveCarCommandHandler removeCarCommandHandler, GetCarWithBrandqueryHandler getCarWithBrandqueryHandler, GetLast5CarsWithBrandQueryHandler getLast5CarsWithBrandQueryHandler)
        {
            _queryHandler = queryHandler;
            _byIdQueryHandler = byIdQueryHandler;
            _createCarCommadHandler = createCarCommadHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _removeCarCommandHandler = removeCarCommandHandler;
            _getCarWithBrandqueryHandler = getCarWithBrandqueryHandler;
            _getLast5CarsWithBrandQueryHandler = getLast5CarsWithBrandQueryHandler;

        }
        [HttpGet]
        public async Task<IActionResult> CarList()
        {
            var values = await _queryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CarById(int id)
        {
            var value = await _byIdQueryHandler.Handle(new GetCarByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(CreateCarCommand command)
        {
            await _createCarCommadHandler.Handle(command);
            return Ok("Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _removeCarCommandHandler.Handle(new RemoveCarCommand(id));
            return Ok("Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand command)
        {
            await _updateCarCommandHandler.Handle(command);
            return Ok("Başarılı Bir Şekilde Güncellendi");
        }
        [HttpGet("GetCarWithBrand")]
        public IActionResult GetCarWithBrand()
        {
            var value = _getCarWithBrandqueryHandler.Hanle();
            return Ok(value);
        }
        [HttpGet("GetLast5CarsWithBrandQuery")]
        public IActionResult GetLast5CarsWithBrandQuery()
        {
            var value = _getLast5CarsWithBrandQueryHandler.Handle();
            return Ok(value);
        }

    }
}
