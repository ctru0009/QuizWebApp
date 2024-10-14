using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class QuizQuestion
    {
        [Key]
        public int QuizQuestionId { get; set; }

        // Foreign key to Quiz
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        // Foreign key to Question
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int QuestionOrder { get; set; }
    }
}
