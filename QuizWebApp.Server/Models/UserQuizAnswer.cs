using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class UserQuizAnswer
    {
        [Key]
        public int UserQuizAnswerId { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign key to Quiz
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        // Foreign key to Question
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        // Foreign key to Answer
        public int SelectedAnswerId { get; set; }
        public Answer SelectedAnswer { get; set; }

        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
