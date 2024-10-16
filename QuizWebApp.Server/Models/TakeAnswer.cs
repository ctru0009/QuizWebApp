using NuGet.Packaging.Signing;
using QuizApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWebApp.Server.Models
{
    public class TakeAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TakeAnswerId { get; set; }

        public int TakeId { get; set; }
        public Take Take { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
}
