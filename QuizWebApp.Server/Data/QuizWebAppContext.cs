using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class QuizWebAppContext : DbContext
    {
        public QuizWebAppContext(DbContextOptions<QuizWebAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<UserQuizAnswer> UserQuizAnswers { get; set; }
        public DbSet<UserQuizScore> UserQuizScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys if needed, relationships, etc.
            modelBuilder.Entity<QuizQuestion>()
                .HasKey(q => new { q.QuizId, q.QuestionId });

            modelBuilder.Entity<UserQuizAnswer>()
                .HasOne(a => a.SelectedAnswer)
                .WithMany()
                .HasForeignKey(a => a.SelectedAnswerId);

            modelBuilder.Entity<UserQuizScore>()
                .HasIndex(uqs => new { uqs.UserId, uqs.QuizId })
                .IsUnique();
        }
    }
}
