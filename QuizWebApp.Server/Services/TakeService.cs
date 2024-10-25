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


            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == take.QuizId);
            var questions = _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            var TimeLimit = quiz.TimeLimit ?? 0;
            var takeCreateResponseDto = new TakeCreateResponseDto
            {
                TakeId = newTake.TakeId,
                QuizId = quiz.QuizId,
                QuizTitle = quiz.QuizTitle,
                StartedAt = newTake.StartedAt,
                TimeLimit = TimeLimit,
                TotalQuestion = questions.Count,
                Questions = new List<TakeQuestionDto>(),
            };

            var takeAnswers = new List<TakeAnswerDto>();
            var takeQuestions = new List<TakeQuestionDto>();

            foreach (var question in questions)
            {
                var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
                var takeQuestion = new TakeQuestionDto
                {
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText,
                    takeAnswerDtos = new List<GetTakeAnswerDto>(),
                };

                foreach (var answer in answers)
                {
                    var takeAnswer = new GetTakeAnswerDto
                    {
                        AnswerId = answer.AnswerId,
                        AnswerText = answer.AnswerText,
                    };
                    takeQuestion.takeAnswerDtos.Add(takeAnswer);
                }
                takeCreateResponseDto.Questions.Add(takeQuestion);
            }
            return takeCreateResponseDto;

        }

        public void SubmitTake(TakeSubmitDto takeSubmitDto)
        {
            //var take = _context.Takes.FirstOrDefault(t => t.TakeId == takeSubmitDto.TakeId);
            //if (take == null)
            //{
            //    return;
            //}

            //// Add the answers to take answers
            //foreach (var answer in takeSubmitDto.TakeAnswers)
            //{
            //    var newTakeAnswer = new TakeAnswer
            //    {
            //        TakeId = take.TakeId,
            //        QuestionId = answer.QuestionId,
            //        AnswerId = answer.AnswerId,
            //    };

            //    _context.TakeAnswers.Add(newTakeAnswer);
            //    take.TakeAnswers.Add(newTakeAnswer);
            //}
            //_context.SaveChanges();

            //// Calculate the score
            //var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == take.QuizId);
            //var questions = _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            //var correctAnswers = 0;
            //foreach (var question in questions) {
            //    var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
            //    var correctAnswer = answers.FirstOrDefault(a => a.IsCorrect == true);
            //    var takeAnswer = take.TakeAnswers.FirstOrDefault(ta => ta.QuestionId == question.QuestionId);
            //    if (takeAnswer != null && takeAnswer.AnswerId == correctAnswer.AnswerId)
            //    {
            //        correctAnswers++;
            //    }
            //}
            //take.Score = correctAnswers;
            //take.CompletedAt = DateTime.UtcNow;
            //take.LastUpdatedAt = DateTime.UtcNow;
            //_context.SaveChanges();

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
