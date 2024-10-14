using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required, MaxLength(100)]
        public string QuizName { get; set; }

        public int? TimeLimit { get; set; } // Time limit in seconds, null if no limit

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to User
        public int CreatedBy { get; set; }
        public User User { get; set; }

        // Navigation property
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<UserQuizAnswer> UserQuizAnswers { get; set; }
        public ICollection<UserQuizScore> UserQuizScores { get; set; }
    }
}
