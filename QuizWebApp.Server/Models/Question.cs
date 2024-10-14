using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to User
        public int CreatedBy { get; set; }
        public User User { get; set; }

        // Navigation property
        public ICollection<Answer> Answers { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
