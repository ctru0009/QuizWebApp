using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Question> Questions { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public ICollection<UserQuizAnswer> UserQuizAnswers { get; set; }
        public ICollection<UserQuizScore> UserQuizScores { get; set; }
    }
}
