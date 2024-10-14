using QuizWebApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int QuestionId { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Answer> Answers { get; set; }
        public ICollection<TakeAnswer> TakeAnswers { get; set; }
    }
}
