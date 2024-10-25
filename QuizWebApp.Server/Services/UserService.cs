using QuizApp.Data;
using QuizWebApp.Server.Interfaces;

namespace QuizWebApp.Server.Services
{
    public class UserService : IUser
    {
        private readonly QuizWebAppContext _context;

        public UserService(QuizWebAppContext context)
        {
            _context = context;
        }
    }
}
