namespace QuizWebApp.Server.Controllers
{

    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
    }

    public class GetQuizzesDto
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedBy { get; set; }

        public IEnumerable<GetQuestionsDto> Questions { get; set; }

    }

    public class GetQuestionsDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<GetAnswersDto> Answers { get; set; }
    }
    public class PutQuestionDto
    {
        public string QuestionText { get; set; }
    }


    public class GetAnswersDto
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
    public class CreateQuizDto
    {
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class UpdateQuizDto
    {
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
    }
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
