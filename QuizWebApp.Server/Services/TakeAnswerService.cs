using QuizApp.Data;
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
    }
}
