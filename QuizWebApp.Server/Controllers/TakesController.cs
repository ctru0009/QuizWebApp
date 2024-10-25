using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakesController : ControllerBase
    {
        private readonly ITake _takeService;

        public TakesController(ITake takeService)
        {
            _takeService = takeService;
        }
        // POST: api/Takes/{quizId}
        [HttpPost("{quizId}")]
        public IActionResult CreateTake(int quizId, [FromBody] TakeQuizDto take)
        {
            var createdTake = _takeService.CreateTake(take);
            return Ok(createdTake);
        }

        // POST: api/Takes/Submit/{takeId}
        [HttpPost("Submit/{takeId}")]
        public IActionResult SubmitTake([FromBody] TakeSubmitDto takeSubmitDto)
        {
            _takeService.SubmitTake(takeSubmitDto);
            return Ok();
        }
    }
}
