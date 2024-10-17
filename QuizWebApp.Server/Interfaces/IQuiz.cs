using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Interfaces
{
    public interface IQuiz
    {
        public List<GetQuizDto> GetQuizzes();

        public GetQuizDto GetQuiz(int id);

        public Quiz CreateQuiz(Quiz quiz);
        public void UpdateQuiz(GetQuizDto quiz);
        public void DeleteQuiz(int id);
    }
}
