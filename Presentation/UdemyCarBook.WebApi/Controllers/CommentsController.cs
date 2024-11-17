using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.RepositoryPattern;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGenericRepository<Comment> _commentsRepository;

        public CommentsController(IGenericRepository<Comment> commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _commentsRepository.GetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateCommand(Comment comment)
        {
            _commentsRepository.Create(comment);
            return Ok("Yorum Başarıyla Eklendi");
        }
        [HttpPut]
        public IActionResult UpdateCommand(Comment comment)
        {
            _commentsRepository.Update(comment);
            return Ok("Yorum Başarıyla Güncellendi");
        }
        [HttpDelete]
        public IActionResult DeleteCommand(int id)
        {
            var value = _commentsRepository.GetById(id);
            _commentsRepository.Remove(value);
            return Ok("Yorum Başarıyla Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _commentsRepository.GetById(id);
            return Ok(value);
        }
    }
}
