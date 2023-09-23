using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EntityConfigurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasKey(q => q.QuizID);

            builder.Property(q => q.Name).IsRequired();
            builder.HasMany(q => q.Questions);

            builder.HasData(new Quiz() { QuizID = 1, Name = "Havuz Soruları", CreatedBy = "Admin"});
        }
    }
}
