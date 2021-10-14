using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AdminPanel.Models;
using OnlineCompetition.Models;

namespace AdminPanel
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Competitions> Competitions { get;set;}
        public DbSet<Questions> Questions { get;set;}
        public DbSet<AnswersMaster> AnswersMaster { get;set;}
        public DbSet<AnswersDetails> AnswersDetails { get;set;}
        public DbSet<CompetitionQuestionsAnswers> CompetitionQuestionsAnswers { get; set; }
        public DbSet<StudentCompetitionQuestionAnswer> StudentCompetitionQuestionAnswer { get; set; }
        public DbSet<CompetitionsUsers> CompetitionsUsers { get;set;}
        public DbSet<Questionnaire> Questionnaire { get;set;}
        public DbSet<QuestionnaireAnswers> QuestionnaireAnswers { get;set;}
        public DbSet<QuestionnaireCategory> QuestionnaireCategory { get;set;}
        public DbSet<QuestionnaireCategoryElements> QuestionnaireCategoryElements { get;set;}
        public DbSet<QuestionnaireUsers> QuestionnaireUsers { get;set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
