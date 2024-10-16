using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.Controllers;

namespace QuizWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizWebAppContext _context;

        public QuizzesController(QuizWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetQuizzesDto>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            var quizzesDto = quizzes.Select(q => new GetQuizzesDto
            {
                QuizId = q.QuizId,
                QuizTitle = q.QuizTitle,
                TimeLimit = q.TimeLimit,
                CreatedBy = q.CreatedBy,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                Questions = q.Questions.Select(q => new GetQuestionsDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt,
                    Answers = q.Answers.Select(a => new GetAnswersDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    })
                })
            });
            return Ok(quizzesDto);
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetQuizzesDto>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            var quizDto = new GetQuizzesDto
            {
                QuizId = quiz.QuizId,
                QuizTitle = quiz.QuizTitle,
                TimeLimit = quiz.TimeLimit,
                CreatedBy = quiz.CreatedBy,
                CreatedAt = quiz.CreatedAt,
                UpdatedAt = quiz.UpdatedAt,
                Questions = quiz.Questions.Select(q => new GetQuestionsDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt,
                    Answers = q.Answers.Select(a => new GetAnswersDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    })
                })
            };
            return Ok(quizDto);
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, UpdateQuizDto updateQuizDto)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            quiz.QuizTitle = updateQuizDto.QuizTitle;
            quiz.TimeLimit = updateQuizDto.TimeLimit;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateQuizDto>> PostQuiz([FromBody] CreateQuizDto quiz)
        {
            var newQuiz = new Quiz
            {
                QuizTitle = quiz.QuizTitle,
                TimeLimit = quiz.TimeLimit,
                CreatedBy = quiz.CreatedBy,
            };
            await _context.SaveChangesAsync();
            return Ok(newQuiz);
        }
        // POST: api/Quizzes/{quizId}/questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{quizId}/questions")]
        public async Task<ActionResult<CreateQuestionDto>> PostQuestion(int quizId, [FromBody] CreateQuestionDto question)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null)
            {
                return NotFound();
            }
            var newQuestion = new Question
            {
                QuestionText = question.QuestionText,
                QuizId = quizId,
                Answers = question.Answers.Select(a => new Answer
                {
                    AnswerText = a.AnswerText,
                    IsCorrect = a.IsCorrect,
                    QuestionId = 0, // Temporary value, will be set after saving
                    QuizId = quizId
                }).ToList()
            };
            _context.Questions.Add(newQuestion);
            await _context.SaveChangesAsync();

            // Update the QuestionId for each answer
            foreach (var answer in newQuestion.Answers)
            {
                answer.QuestionId = newQuestion.QuestionId;
            }
            await _context.SaveChangesAsync();

            return Ok(newQuestion);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }
    }
}
