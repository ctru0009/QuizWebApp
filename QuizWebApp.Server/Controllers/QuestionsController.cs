using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestion _questionService;

        public QuestionsController(IQuestion questionService)
        {
            _questionService = questionService;
        }

        // GET: api/Questions
        [HttpGet]
        public IActionResult Get()
        {
            var questions = _questionService.GetQuestions();

            return Ok(questions);
        }
        // GET: api/Questions/Quizzes/{quizId}
        [HttpGet]
        [Route("Quizzes/{quizId}")] // "api/Questions/Quizzes/{quizId}
        public IActionResult GetQuestionsByQuizId(int quizId)
        {
            var questions = _questionService.GetQuestionsByQuizId(quizId);
            return Ok(questions);
        }
        // GET: api/Questions/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var question = _questionService.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        // POST: api/Questions
        [HttpPost]
        public IActionResult CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            if (createQuestionDto == null)
            {
                return BadRequest("Question is required to create question");
            }

            if (createQuestionDto.createAnswersDto == null)
            {
                return BadRequest("Answers are required to create question");
            }
            var newQuestion = new Question
            {
                QuestionText = createQuestionDto.QuestionText,
                QuizId = createQuestionDto.QuizId,
                Answers = new List<Answer>(),
                TakeAnswers = new List<TakeAnswer>()
            };

            var newAnswers = new List<Answer>();
            foreach (var answer in createQuestionDto.createAnswersDto)
            {
                var newAnswer = new Answer
                {
                    AnswerText = answer.AnswerText,
                    IsCorrect = answer.IsCorrect,
                    QuestionId = newQuestion.QuestionId,
                    QuizId = newQuestion.QuizId
                };
                newAnswers.Add(newAnswer);
            }

            newQuestion.Answers = newAnswers;
            var createdQuestion = _questionService.CreateQuestion(newQuestion);

            var newcreatedQuestion = new CreateQuestionDto
            {
                QuizId = createdQuestion.QuizId,
                QuestionText = createdQuestion.QuestionText,
                createAnswersDto = newAnswers.Select(a => new CreateAnswersDto
                {
                    AnswerText = a.AnswerText,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
            return CreatedAtAction(nameof(Get), createdQuestion.QuestionId, newcreatedQuestion);
        }
    }

}
