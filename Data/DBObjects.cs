using GameShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data
{
    public class DBObjects
    {
        public static async Task Initialize(AppDBContent content) 
        {
            await content.Database.MigrateAsync();

            if (!await content.DbCategory.AnyAsync())
            {
                content.DbCategory.AddRange(GetCategories()); // if DbCategory is empty, initialize list of categories automatically
                
                await content.SaveChangesAsync();
            }

            if (!await content.DbGame.AnyAsync()) // if DbGame is empty, initialize list of items automatically
            {
                content.AddRange(
                    new Game { Image = "/img/css.jpg", Name = "Counter-Strike: Source", Desc = "Неустаревающий командный тактический шутер", Details = "", TechReq = "", IsAvailable = true, Quantity = 15, Price = 300, CategoryId = 1 },
                    new Game { Image = "/img/cof.jpg", Name = "Cry of Fear", Desc = "Известный представитель жанра survival horror", Details = "", TechReq = "", IsAvailable = true, Quantity = 20, Price = 100, CategoryId = 2 },
                    new Game { Image = "/img/ylad.jpg", Name = "Yakuza: Like a Dragon", Desc = "Восьмой выпуск легендарной серии в стиле JRPG", Details = "", TechReq = "", IsAvailable = true, Quantity = 10, Price = 800, CategoryId = 3 },
                    new Game { Image = "/img/va11halla.jpg", Name = "Va-11 Hall-a", Desc = "Симулятор бармена эпохи киберпанка", Details = "", TechReq = "", IsAvailable = false, Quantity = 0, Price = 200, CategoryId = 4 },
                    new Game { Image = "/img/os.jpg", Name = "One Shot", Desc = "Сюрреалистическая приключенческая игра", Details = "", TechReq = "", IsAvailable = true, Quantity = 7, Price = 50, CategoryId = 5 }) ;
                
                await content.SaveChangesAsync();
            }
        }

        private static IEnumerable<Category> GetCategories()
        {
            var list = new List<Category>()
            {
                new Category { CategoryName = "Action", CategoryDesc = "Для желающих проходить один и тот же момент тысячу раз" },
                new Category { CategoryName = "Horror", CategoryDesc = "Для любителей пощекотать нервишки" },
                new Category { CategoryName = "JRPG", CategoryDesc = "Японские RPG" },
                new Category { CategoryName = "RPG", CategoryDesc = "Жанр, основанный на элементах настольных ролевых игр" },
                new Category { CategoryName = "Quest", CategoryDesc = "Искусство создания интерактивных историй" }
            };

            return list;
        }
    }
}
