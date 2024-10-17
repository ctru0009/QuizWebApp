using Microsoft.EntityFrameworkCore;
using QuizWebApp.Server.Models;

namespace QuizApp.Data
{
    public class QuizWebAppContext : DbContext
    {
        public QuizWebAppContext(DbContextOptions<QuizWebAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Take> Takes { get; set; }
        public DbSet<TakeAnswer> TakeAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Quiz
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.User)
                .WithMany(u => u.Quizzes)
                .HasForeignKey(q => q.UserId);

            // Question
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.QuizId);

            // Answer
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Quiz)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuizId);

            // Take
            modelBuilder.Entity<Take>()
                .HasOne(t => t.Quiz)
                .WithMany(q => q.Takes)
                .HasForeignKey(t => t.QuizId);

            modelBuilder.Entity<Take>()
                .HasOne(t => t.User)
                .WithMany(u => u.Takes)
                .HasForeignKey(t => t.UserId);

            // TakeAnswer
            modelBuilder.Entity<TakeAnswer>()
                .HasOne(ta => ta.Take)
                .WithMany(t => t.TakeAnswers)
                .HasForeignKey(ta => ta.TakeId);

            modelBuilder.Entity<TakeAnswer>()
                .HasOne(ta => ta.Answer)
                .WithMany(a => a.TakeAnswers)
                .HasForeignKey(ta => ta.AnswerId);

            modelBuilder.Entity<TakeAnswer>()
                .HasOne(ta => ta.Question)
                .WithMany(q => q.TakeAnswers)
                .HasForeignKey(ta => ta.QuestionId);

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Email = "admin@gmail.com"
                }
            );


        }
    }
}
