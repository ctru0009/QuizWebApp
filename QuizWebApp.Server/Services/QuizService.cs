using QuizApp.Data;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Services
{
    public class QuizService : IQuiz
    {
        private readonly QuizWebAppContext _context;
        public QuizService(QuizWebAppContext context)
        {
            _context = context;
        }
        public List<GetQuizDto> GetQuizzes()
        {
            var quizzes = _context.Quizzes.ToList();
            if (quizzes == null)
            {
                return null;
            }
            var getQuizzesDto = new List<GetQuizDto>();
            foreach (var quiz in quizzes)
            {
                var getQuizDto = new GetQuizDto
                {
                    QuizId = quiz.QuizId,
                    QuizTitle = quiz.QuizTitle,
                    UserId = quiz.UserId,
                    TimeLimit = quiz.TimeLimit,
                    CreatedAt = quiz.CreatedAt,
                    UpdatedAt = quiz.UpdatedAt
                };
                getQuizzesDto.Add(getQuizDto);
            }
            return getQuizzesDto;
        }

        public GetQuizDto GetQuiz(int id)
        {
            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == id);
            if (quiz == null)
            {
                return null;
            }

            var questions = _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            quiz.Questions = questions;
            foreach (var question in questions)
            {
                var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
                question.Answers = answers;
            }
            var getQuizDto = new GetQuizDto
            {
                QuizId = quiz.QuizId,
                QuizTitle = quiz.QuizTitle,
                UserId = quiz.UserId,
                TimeLimit = quiz.TimeLimit,
                Questions = questions.Select(q => new GetQuestionDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    Answers = q.Answers.Select(a => new GetAnswerDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };
            return getQuizDto;
        }

        public Quiz CreateQuiz(Quiz quiz)
        {
            var newQuiz = _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return newQuiz.Entity;
        }

        public void UpdateQuiz(GetQuizDto quiz)
        {
            var existingQuiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == quiz.QuizId);
            existingQuiz.QuizTitle = quiz.QuizTitle;
            existingQuiz.TimeLimit = quiz.TimeLimit;
            existingQuiz.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public void DeleteQuiz(int id)
        {
            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == id);
            if (quiz == null)
            {
                return;
            }
            _context.Quizzes.Remove(quiz);
            _context.SaveChanges();
        }
    }
}
