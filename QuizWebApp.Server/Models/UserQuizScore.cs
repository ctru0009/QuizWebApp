using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class UserQuizScore
    {
        [Key]
        public int UserQuizScoreId { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign key to Quiz
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
