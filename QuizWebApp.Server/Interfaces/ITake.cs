using QuizWebApp.Server.DTOs;
using QuizWebApp.Server.Models;

namespace QuizWebApp.Server.Interfaces
{
    public interface ITake
    {
        public TakeCreateResponseDto CreateTake(TakeQuizDto take);
        public void SubmitTake(TakeSubmitDto takeSubmitDto);

    }
}
