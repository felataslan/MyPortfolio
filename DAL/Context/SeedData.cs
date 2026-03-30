using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.DAL.Context
{
    public static class SeedData
    {
        public static void Initialize(MyPortfolioContext context)
        {
            context.Database.Migrate();

            if (!context.Abouts.Any())
            {
                context.Abouts.Add(new About
                {
                    Title = "Hakkımda",
                    SubDescription = "Full-Stack Yazılım Geliştirici",
                    Details = "Modern web teknolojileri üzerine çalışıyorum. ASP.NET Core MVC ve Razor kullanarak performanslı ve ölçeklenebilir uygulamalar geliştiriyorum."
                });
            }

            if (!context.Features.Any())
            {
                context.Features.Add(new Feature
                {
                    Title = "Yazılım Geliştirici Portfolio",
                    Description = "Projelerimi ve yeteneklerimi keşfedin."
                });
            }

            if (!context.Skills.Any())
            {
                context.Skills.Add(new Skill { Title = "C# / ASP.NET Core", Value = 95 });
                context.Skills.Add(new Skill { Title = "MSSQL / SQLite", Value = 85 });
                context.Skills.Add(new Skill { Title = "HTML / CSS / JS", Value = 90 });
            }

            if (!context.Portfolios.Any())
            {
                context.Portfolios.Add(new Portfolio
                {
                    Title = "Muhasebe Yazılımı",
                    SubTitle = "Web Uygulaması",
                    ImageUrl = "https://via.placeholder.com/600x400",
                    Url = "#",
                    Description = "Gelişmiş finansal işlemler yapan ERP tabanlı muhasebe uygulaması."
                });
            }

            if (!context.Experiences.Any())
            {
                context.Experiences.Add(new Experience
                {
                    Head = "Yazılım Mühendisi",
                    Title = "X Teknoloji A.Ş.",
                    Date = "2021 - Günümüz",
                    Description = "Kurumsal web uygulamalarının geliştirilmesi."
                });
            }

            context.SaveChanges();
        }
    }
}
