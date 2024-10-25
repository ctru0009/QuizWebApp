using QuizWebApp.Server.DTOs;

namespace QuizWebApp.Server.Interfaces
{
    public interface ITakeAnswer
    {
        public TakeSubmitResponseDto getResult(int takeId);

        public List<TakeSubmitResponseDto> getResults();
    }
}
