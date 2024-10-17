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
    }
}
