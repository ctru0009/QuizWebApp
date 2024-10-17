using QuizApp.Data;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Services
{
    public class TakeService : ITake
    {
        private readonly QuizWebAppContext _context;

        public TakeService(QuizWebAppContext context)
        {
            _context = context;
        }

        public TakeCreateResponseDto CreateTake(TakeQuizDto take)
        {
            var newTake = new Take
            {
                QuizId = take.QuizId,
                UserId = take.UserId,
                StartedAt = DateTime.UtcNow,
                TakeAnswers = new List<TakeAnswer>(),
                Score = 0,
            };

            _context.Takes.Add(newTake);
            _context.SaveChanges();

            var timeLimit = 0;
            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == take.QuizId);
            if (quiz == null || quiz.TimeLimit == null)
            {
                timeLimit = 0;
            }
            else
            {
                timeLimit = quiz.TimeLimit.Value;
            }

            var takeResponse = new TakeCreateResponseDto
            {
                TakeId = newTake.TakeId,
                QuizId = newTake.QuizId,
                StartedAt = newTake.StartedAt,
                TimeLimit = timeLimit,
            };

            return takeResponse;

        }

        public void SubmitTake(TakeSubmitDto takeSubmitDto)
        {
            var take = _context.Takes.FirstOrDefault(t => t.TakeId == takeSubmitDto.TakeId);
            if (take == null)
            {
                return;
            }

            // Add the answers to take answers
            foreach (var answer in takeSubmitDto.TakeAnswers)
            {
                var newTakeAnswer = new TakeAnswer
                {
                    TakeId = take.TakeId,
                    QuestionId = answer.QuestionId,
                    AnswerId = answer.AnswerId,
                };
                
                _context.TakeAnswers.Add(newTakeAnswer);
                take.TakeAnswers.Add(newTakeAnswer);
            }
            _context.SaveChanges();

            // Calculate the score
            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == take.QuizId);
            var questions = _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            var correctAnswers = 0;
            foreach (var question in questions) {
                var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
                var correctAnswer = answers.FirstOrDefault(a => a.IsCorrect == true);
                var takeAnswer = take.TakeAnswers.FirstOrDefault(ta => ta.QuestionId == question.QuestionId);
                if (takeAnswer != null && takeAnswer.AnswerId == correctAnswer.AnswerId)
                {
                    correctAnswers++;
                }
            }
            take.Score = correctAnswers;
            take.CompletedAt = DateTime.UtcNow;
            take.LastUpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
