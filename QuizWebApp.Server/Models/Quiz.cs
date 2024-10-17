using QuizWebApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWebApp.Server.Models
{
    public class Quiz
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int? TimeLimit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }
        // Navigation property
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers{ get; set; }
        public ICollection<Take> Takes { get; set; }
    }
}
