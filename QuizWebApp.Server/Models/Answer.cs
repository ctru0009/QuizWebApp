using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        // Foreign key to Question
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
