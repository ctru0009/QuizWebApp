using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuiz _quizService;

        public QuizzesController(IQuiz quizService)
        {
            _quizService = quizService;
        }

        // GET: api/Quizzes
        [HttpGet]   
        public IActionResult Get()
        {
            var quizzes = _quizService.GetQuizzes();
            return Ok(quizzes);
        }

        // GET api/Quizzes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = _quizService.GetQuiz(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        // POST api/Quizzes
        [HttpPost]
        public IActionResult Post([FromBody] CreateQuizDto createQuizDto)
        {
            var newQuiz = new Quiz
            {
                QuizTitle = createQuizDto.QuizTitle,
                UserId = createQuizDto.UserId,
                TimeLimit = createQuizDto.TimeLimit,
                Questions = new List<Question>(),
                Answers = new List<Answer>(),
                Takes = new List<Take>()
            };
            var createdQuiz = _quizService.CreateQuiz(newQuiz);
            return CreatedAtAction(nameof(Get), new { id = createdQuiz.QuizId }, createdQuiz);
        }

        // PUT api/Quizzes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateQuizDto updateQuizDto)
        {
            var quiz = _quizService.GetQuiz(id);
            if (quiz == null)
            {
                return NotFound();
            }
            quiz.QuizTitle = updateQuizDto.QuizTitle;
            quiz.TimeLimit = updateQuizDto.TimeLimit;
            quiz.UpdatedAt = DateTime.UtcNow;
            _quizService.UpdateQuiz(quiz);
            return Ok("Update successfully");
        }
    }


}
