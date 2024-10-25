using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Server.Interfaces;

namespace QuizWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakeAnswersController : ControllerBase
    {
        private readonly ITakeAnswer _takeAnswerService;

        public TakeAnswersController(ITakeAnswer takeAnswerService)
        {
            _takeAnswerService = takeAnswerService;
        }

        [HttpGet]
        // GET: api/TakeAnswers/{takeId}
        [Route("{takeId}")]
        public IActionResult GetTakeAnswers(int takeId)
        {
            var takeAnswers = _takeAnswerService.getResult(takeId);
            return Ok(takeAnswers);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var takeAnswers = _takeAnswerService.getResults();
            return Ok(takeAnswers);
        }

    }
}
