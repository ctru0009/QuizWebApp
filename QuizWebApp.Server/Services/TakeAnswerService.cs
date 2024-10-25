using QuizApp.Data;
using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Interfaces;

namespace QuizWebApp.Server.Services
{
    public class TakeAnswerService : ITakeAnswer
    {
        private readonly QuizWebAppContext _context;

        public TakeAnswerService(QuizWebAppContext context)
        {
            _context = context;
        }

        public TakeSubmitResponseDto getResult(int takeId)
        {
            var take = _context.Takes.FirstOrDefault(t => t.TakeId == takeId);
            var takeAnswers = _context.TakeAnswers.Where(ta => ta.TakeId == takeId).ToList();
            var quiz = _context.Quizzes.FirstOrDefault(q => q.QuizId == take.QuizId);
            var questions = _context.Questions.Where(q => q.QuizId == quiz.QuizId).ToList();
            if (!take.CompletedAt.HasValue)
            {
                throw new InvalidOperationException("The take has not been completed yet.");
            }

            var takeSubmitResponseDto = new TakeSubmitResponseDto
            {
                TakeId = take.TakeId,
                QuizId = quiz.QuizId,
                QuizTitle = quiz.QuizTitle,
                StartTime = take.StartedAt,
                SubmitTime = (DateTime)take.CompletedAt,
                TimeTaken = (int)(take.CompletedAt - take.StartedAt).Value.TotalSeconds,
                TotalQuestion = questions.Count,
                Score = take.Score,
                Results = new List<ResultSubmit>(),
            };

            foreach (var question in questions)
            {
                var answers = _context.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
                var takeAnswer = takeAnswers.FirstOrDefault(ta => ta.QuestionId == question.QuestionId);
                if (takeAnswer == null)
                {
                    // Handle the case where takeAnswer is null, e.g., skip this question or set default values
                    continue;
                }
                var resultSubmit = new ResultSubmit
                {
                    QuestionId = question.QuestionId,
                    AnswerId = takeAnswer.AnswerId,
                    AnswerText = answers.FirstOrDefault(a => a.AnswerId == takeAnswer.AnswerId).AnswerText,
                    CorrectAnswerId = answers.FirstOrDefault(a => a.IsCorrect).AnswerId,
                    CorrectAnswerText = answers.FirstOrDefault(a => a.IsCorrect).AnswerText,
                    IsCorrect = takeAnswer.AnswerId == answers.FirstOrDefault(a => a.IsCorrect).AnswerId ? true : false,
                };
                takeSubmitResponseDto.Results.Add(resultSubmit);
            }
            return takeSubmitResponseDto;

        }

        public List<TakeSubmitResponseDto> getResults()
        {
            var takeAnswers = _context.TakeAnswers.ToList();
            var takeSubmitResponseDtos = new List<TakeSubmitResponseDto>();
            foreach (var takeAnswer in takeAnswers)
            {
                var takeSubmitResponseDto = getResult(takeAnswer.TakeId);
                takeSubmitResponseDtos.Add(takeSubmitResponseDto);
            }
            return takeSubmitResponseDtos;
        }
    }
}
