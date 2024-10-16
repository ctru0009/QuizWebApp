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
    public class QuestionsController : ControllerBase
    {
        private readonly QuizWebAppContext _context;

        public QuestionsController(QuizWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetQuestionsDto>>> GetQuestions()
        {
            var questions = await _context.Questions.ToListAsync();

            var questionsDto = questions.Select(q => new GetQuestionsDto
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
            });
            return Ok(questionsDto);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetQuestionsDto>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            var questionDto = new GetQuestionsDto
            {
                QuestionId = question.QuestionId,
                QuestionText = question.QuestionText,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                Answers = question.Answers.Select(a => new GetAnswersDto
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText,
                    IsCorrect = a.IsCorrect
                })
            };
            return Ok(questionDto);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, PutQuestionDto putQuestionDto) {

            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            try
            {
                question.QuestionText = putQuestionDto.QuestionText;
                question.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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
        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
