using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Interfaces
{
    public interface IQuestion
    {
        public List<GetQuestionDto> GetQuestions();

        public GetQuestionDto GetQuestion(int id);

        public Question CreateQuestion(Question question);

        public List<GetQuestionDto> GetQuestionsByQuizId(int quizId);

    }
}
