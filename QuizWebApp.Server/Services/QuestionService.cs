using QuizApp.Data;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;
using QuizWebApp.Server.DTOs;
using Microsoft.EntityFrameworkCore;

namespace QuizWebApp.Server.Services
{
    public class QuestionService : IQuestion
    {
        private readonly QuizWebAppContext _context;
        public QuestionService(QuizWebAppContext context)
        {
            _context = context;
        }
        public Question CreateQuestion(Question question)
        {
            // Add the answers in question to the database
            var answers = question.Answers;
            foreach (var answer in answers)
            {
                _context.Answers.Add(answer);
            }
            var newQuestion = _context.Questions.Add(question);
            _context.SaveChanges();
            return newQuestion.Entity;
        }

        public GetQuestionDto GetQuestion(int id)
        {
            var question = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return null;
            }
            var questionAnswers = _context.Answers.Include(a => a.Question).Where(a => a.QuestionId == question.QuestionId).ToList();
            question.Answers = questionAnswers;
            var getQuestionDto = new GetQuestionDto
            {
                QuestionId = question.QuestionId,
                QuestionText = question.QuestionText,
                Answers = questionAnswers.Select(a => new GetAnswerDto
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };
            return getQuestionDto;
        }

        public List<GetQuestionDto> GetQuestions()
        {
            var questions = _context.Questions.ToList();
            if (questions == null)
            {
                return null;
            }

            var getQuestionsDto = new List<GetQuestionDto>();
            foreach ( var question in questions) {
                var questionAnswers = _context.Answers.Include(a => a.Question).Where(a => a.QuestionId == question.QuestionId).ToList();
                question.Answers = questionAnswers;
                var getQuestionDto = new GetQuestionDto
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    Answers = questionAnswers.Select(a => new GetAnswerDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                };
                getQuestionsDto.Add(getQuestionDto);
            }
            return getQuestionsDto;
        }

        public List<GetQuestionDto> GetQuestionsByQuizId(int quizId)
        {
            var questions = _context.Questions.Where(q => q.QuizId == quizId).ToList();
            if (questions == null)
            {
                return null;
            }

            var getQuestionsDto = new List<GetQuestionDto>();

            foreach ( var question in questions) {
                var questionAnswers = _context.Answers.Include(a => a.Question).Where(a => a.QuestionId == question.QuestionId).ToList();
                question.Answers = questionAnswers;
                var getQuestionDto = new GetQuestionDto
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    Answers = questionAnswers.Select(a => new GetAnswerDto
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                };
                getQuestionsDto.Add(getQuestionDto);
            }
            return getQuestionsDto;
        }

        public void DeleteQuestion(int id)
        {
            var question = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return;
            }
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }
    }
}
