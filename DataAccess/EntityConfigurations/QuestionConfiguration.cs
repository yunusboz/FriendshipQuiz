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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.QuestionID);
            builder.Property(q => q.QuestionText).IsRequired();
            builder.Property(q => q.OptionA).IsRequired();
            builder.Property(q => q.OptionB).IsRequired();
            builder.Property(q => q.OptionC).IsRequired();
            builder.Property(q => q.OptionD).IsRequired();
            builder.Property(q => q.OptionE).IsRequired();
            builder.Property(q => q.CorrectAnswer).IsRequired();

            builder.HasOne(q => q.Quiz);

            builder.HasData(
                new Question() 
                { 
                    QuestionID = 1,
                    QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                    QuestionText = "En sevdiği yemek?",
                    OptionA = "Patates Kızartması",
                    OptionB = "Burger",
                    OptionC = "Döner",
                    OptionD = "Kuru Fasulye",
                    OptionE = "Makarna",
                    CorrectAnswer = 3 
                },
                new Question() 
                {
                    QuestionID = 2,
                    QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                    QuestionText = "En sevdiği müzik türü?",
                    OptionA = "Pop",
                    OptionB = "Rap",
                    OptionC = "Rock",
                    OptionD = "Türk Halk Müziği",
                    OptionE = "Arabesk",
                    CorrectAnswer = 1
                },
                new Question() 
                {
                    QuestionID = 3,
                    QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                    QuestionText = "Zamanını nasıl geçirir?",
                    OptionA = "Uyuyarak",
                    OptionB = "Bilgisayar başında",
                    OptionC = "Yürüyüş yaparak",
                    OptionD = "Kitap okuyarak",
                    OptionE = "Arkadaşlarıyla buluşarak",
                    CorrectAnswer = 4
                },
                new Question() 
                {
                    QuestionID = 4,
                    QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                    QuestionText = "Onu en çok ne sevindirir?",
                    OptionA = "Kayıp para bulmak",
                    OptionB = "Tuttuğu takımın galibiyeti",
                    OptionC = "Süpriz hediye almak",
                    OptionD = "Alışveriş mağazasındaki indirimler",
                    OptionE = "Çekilişle telefon kazanmak",
                    CorrectAnswer = 2
                },
                new Question() 
                { 
                    QuestionID = 5,
                    QuizID = Guid.Parse("ea8b887f-042d-4c56-a034-68845aa34099"),
                    QuestionText = "Favori rengi ne?",
                    OptionA = "Kırmızı",
                    OptionB = "Beyaz",
                    OptionC = "Siyah",
                    OptionD = "Sarı",
                    OptionE = "Mavi",
                    CorrectAnswer = 5
                }
                );
                
        }
    }
}
