using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;
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
            // Set relationship between User and Quiz
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.User)
                .WithMany(u => u.Quizzes)
                .HasForeignKey(q => q.CreatedBy);

            // Set relationship between Take and User
            modelBuilder.Entity<Take>()
                            .HasOne(t => t.User)
                            .WithMany(u => u.Takens)
                            .HasForeignKey(t => t.UserId);

            // Set relationship between Quiz and Question
            modelBuilder.Entity<Question>()
                            .HasOne(q => q.Quiz)
                            .WithMany(q => q.Questions)
                            .HasForeignKey(q => q.QuizId)
                            .OnDelete(DeleteBehavior.Cascade);
            // Set relationship between Answer and Question
            modelBuilder.Entity<Answer>()
                            .HasOne(a => a.Question)
                            .WithMany(q => q.Answers)
                            .HasForeignKey(a => a.QuestionId)
                            .OnDelete(DeleteBehavior.Cascade);
            // Set relationship between Answer and Quiz
            modelBuilder.Entity<Answer>()
                            .HasOne(a => a.Quiz)
                            .WithMany(q => q.Answers)
                            .HasForeignKey(a => a.QuizId)
                            .OnDelete(DeleteBehavior.Cascade);
            // Set relationship between Take and Quiz
            modelBuilder.Entity<Take>()
                            .HasOne(t => t.Quiz)
                            .WithMany(q => q.Takens)
                            .HasForeignKey(t => t.QuizId)
                            .OnDelete(DeleteBehavior.Cascade);
            // Set relationship between TakeAnswer and Take
            modelBuilder.Entity<TakeAnswer>()
                            .HasOne(ta => ta.Take)
                            .WithMany(t => t.TakeAnswers)
                            .HasForeignKey(ta => ta.TakeId)
                            .OnDelete(DeleteBehavior.Cascade);
            // Set relationship between TakeAnswer and Answer
            modelBuilder.Entity<TakeAnswer>()
                            .HasOne(ta => ta.Answer)
                            .WithMany(a => a.TakeAnswers)
                            .HasForeignKey(ta => ta.AnswerId);
            // Set relationship between TakeAnswer and Question
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
