
namespace QuizWebApp.Server.DTOs
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
        public DateTime CreatedAt { get; set; }
    }
    public class CreateQuizDto
    {
        public string QuizTitle { get; set; }
        public int UserId { get; set; }
        public int? TimeLimit { get; set; }
    }

    public class UpdateQuizDto
    {
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
    }
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; }
        public int QuizId { get; set; }
        public List<CreateAnswersDto> createAnswersDto { get; set; }
    }

    public class CreateAnswersDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class GetQuizDto
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public List<GetQuestionDto> Questions { get; set; }
    }

    public class GetQuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<GetAnswerDto> Answers { get; set; }
    }

    public class GetAnswerDto
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class TakeQuizDto
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
    }

    public class TakeCreateResponseDto
    {
        public int TakeId { get; set; }
        public int QuizId { get; set; }
        public DateTime StartedAt { get; set; }
        public int TimeLimit { get; set; }
    }

    public class TakeSubmitDto
    {
        public int TakeId { get; set; }
        public List<TakeAnswerDto> TakeAnswers { get; set; }
    }

    public class TakeAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

    public class GetTakeDto
    {
        public int TakeId { get; set; }
        public int QuizId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }
        public List<TakeAnswerDto> TakeAnswers { get; set; }
    }
}
