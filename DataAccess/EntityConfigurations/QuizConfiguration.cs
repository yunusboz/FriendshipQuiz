using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfigurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasKey(q => q.QuizID);

            builder.Property(q => q.Name).IsRequired().HasMaxLength(80);
            builder.HasMany(q => q.Questions);
            builder.HasMany(q => q.QuizResults);
            builder.HasOne(q => q.ApplicationUser)
                .WithMany(a => a.Quizzes)
                .HasForeignKey(a => a.AppUserId);

            builder.HasData(new Quiz() 
            { 
                QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                Name = "Havuz Soruları",
                CreatedBy = "Admin"
            });
        }
    }
}
