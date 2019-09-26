using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieSearch.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Movie> Movies { get; set; }

        //public MovieDbContext() : base("name=MovieDbContext")
        //{
        //    Database.SetInitializer(new MovieDbInitializer());
        //}

        public class MovieDbInitializer : CreateDatabaseIfNotExists<MovieDbContext>
        {
            protected override void Seed(MovieDbContext db)
            {
                string[] actors = new string[] { "Алан Рикман",
                    "Бенедикт Камбербэтч", "Вигго Мортенсен", "Джеймс МакЭвой",
                    "Джейк Джилленхол", "Кристиан Бэйл", "Леонардо ДиКаприо",
                    "Майкл Фассбендер", "Мэттью МакКонахи", "Райан Гослинг",
                    "Том Харди", "Хит Леджер", "Хоакин Феникс", "Анджелина Джоли",
                    "Энн Хэтэуэй", "Дженнифер Лоуренс", "Джессика Честейн", "Кейт Уинслет",
                    "Марион Котийяр", "Моника Беллуччи", "Тильда Суинтон", "Эми Адамс", "Эмма Стоун" };

                for (int i = 0; i < actors.Length; i++)
                    db.Actors.Add(new Actor() { Id = i, Name = actors[i] });

                string[] genres = new string[] { "Аниме", "Боевик", "Детектив", "Драма", "Комедия",
                    "Криминал", "Мелодрама", "Приключения", "Триллер"};

                for (int i = 0; i < genres.Length; i++)
                    db.Genres.Add(new Genre() { Id = i, Name = genres[i] });

                string[] countries = new string[] { "США", "Англия", "Франция", "Австралия", "Китай" };

                for (int i = 0; i < countries.Length; i++)
                    db.Countries.Add(new Country() { Id = i, Name = countries[i] });

                db.SaveChanges();

                string[] movies = new string[] { "Побег из Шоушенка", "Темный рыцарь", "Властелин колец: Братство Кольца",
                    "Бойцовский клуб", "Начало", "Зеленая миля", "Вечное сияние чистого разума", "Крестный отец",
                    "Сияние", "Леон", "Криминальное чтиво", "Престиж", "Интерстеллар", "Гладиатор", "Назад в будущее",
                    "Матрица", "Отступники", "Поймай меня, если сможешь", "Остров проклятых", "Титаник",
                    "Хатико: Самый верный друг", "Игра", "Эффект бабочки" };

                Random random = new Random();

                for (int i = 0; i < movies.Length; i++)
                {
                    List<Genre> listGenres = new List<Genre>();
                    List<Actor> listActors = new List<Actor>();
                    int countyId = random.Next(1, countries.Length); 

                    for (int j = 0; j < genres.Length; j++)
                        if (random.Next(0, 2) == 1)
                            listGenres.Add(db.Genres.Find(j));

                    for (int z = 0; z < actors.Length; z++)
                        if (random.Next(0, 2) == 1)
                            listActors.Add(db.Actors.Find(z));

                    db.Movies.Add(new Movie()
                    {
                        Id = i,
                        Name = movies[i],
                        Year = random.Next(1985, 2019),
                        Country = db.Countries.Find(countyId),
                        Genres = listGenres,
                        Actors = listActors
                    });
                }

                /*
                Actor actor1 = db.Actors.Add(new Actor() { Id = 0, Name = "Алан Рикман" });
                Actor actor2 = db.Actors.Add(new Actor() { Id = 1, Name = "Бенедикт Камбербэтч" });
                Actor actor3 = db.Actors.Add(new Actor() { Id = 2, Name = "Вигго Мортенсен" });
                Actor actor4 = db.Actors.Add(new Actor() { Id = 3, Name = "Джеймс МакЭвой" });
                Actor actor5 = db.Actors.Add(new Actor() { Id = 4, Name = "Джейк Джилленхол" });
                Actor actor6 = db.Actors.Add(new Actor() { Id = 5, Name = "Кристиан Бэйл" });
                Actor actor7 = db.Actors.Add(new Actor() { Id = 6, Name = "Леонардо ДиКаприо" });
                Actor actor8 = db.Actors.Add(new Actor() { Id = 7, Name = "Майкл Фассбендер" });
                Actor actor9 = db.Actors.Add(new Actor() { Id = 8, Name = "Мэттью МакКонахи" });
                Actor actor10 = db.Actors.Add(new Actor() { Id = 9, Name = "Райан Гослинг" });
                Actor actor11 = db.Actors.Add(new Actor() { Id = 10, Name = "Том Харди" });
                Actor actor12 = db.Actors.Add(new Actor() { Id = 11, Name = "Хит Леджер" });
                Actor actor13 = db.Actors.Add(new Actor() { Id = 12, Name = "Хоакин Феникс" });
                Actor actor14 = db.Actors.Add(new Actor() { Id = 13, Name = "Анджелина Джоли" });
                Actor actor15 = db.Actors.Add(new Actor() { Id = 14, Name = "Энн Хэтэуэй" });

                Genre genre1 = db.Genres.Add(new Genre() { Id = 0, Name = "Аниме" });
                Genre genre2 = db.Genres.Add(new Genre() { Id = 1, Name = "Боевик" });
                Genre genre3 = db.Genres.Add(new Genre() { Id = 2, Name = "Детектив" });
                Genre genre4 = db.Genres.Add(new Genre() { Id = 3, Name = "Драма" });
                Genre genre5 = db.Genres.Add(new Genre() { Id = 4, Name = "Комедия" });
                Genre genre6 = db.Genres.Add(new Genre() { Id = 5, Name = "Криминал" });
                Genre genre7 = db.Genres.Add(new Genre() { Id = 6, Name = "Мелодрама" });
                Genre genre8 = db.Genres.Add(new Genre() { Id = 7, Name = "Приключения" });
                Genre genre9 = db.Genres.Add(new Genre() { Id = 8, Name = "Триллер" });

                Country country1 = db.Countries.Add(new Country() { Id = 0, Name = "США" });
                Country country2 = db.Countries.Add(new Country() { Id = 1, Name = "Англия" });
                Country country3 = db.Countries.Add(new Country() { Id = 2, Name = "Франция" });
                Country country4 = db.Countries.Add(new Country() { Id = 3, Name = "Австралия" });
                Country country5 = db.Countries.Add(new Country() { Id = 4, Name = "Китай" });
                                
                db.Movies.Add(new Movie() {
                    Id = 0,
                    Name = "Побег из Шоушенка",
                    Year = 1994,
                    Country = country1,
                    Genres = new List<Genre>
                        { genre1, genre3, genre4, genre7, genre9 },
                    Actors = new List<Actor>
                        { actor1, actor3, actor5, actor7, actor11, actor13, actor15 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 1,
                    Name = "Темный рыцарь",
                    Year = 2008,
                    Country = country2,
                    Genres = new List<Genre>
                        { genre2, genre5, genre6, genre8, genre9 },
                    Actors = new List<Actor>
                        { actor2, actor4, actor6, actor8, actor9, actor10, actor12, actor14 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 2,
                    Name = "Властелин колец: Братство Кольца",
                    Year = 2001,
                    Country = country3,
                    Genres = new List<Genre>
                        { genre1, genre3, genre4, genre7, genre8 },
                    Actors = new List<Actor>
                        { actor1, actor3, actor5, actor7, actor11, actor13, actor15 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 3,
                    Name = "Бойцовский клуб",
                    Year = 1999,
                    Country = country4,
                    Genres = new List<Genre>
                        { genre2, genre5, genre6, genre8, genre9 },
                    Actors = new List<Actor>
                        { actor2, actor4, actor6, actor8, actor9, actor10, actor12, actor14 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 4,
                    Name = "Начало",
                    Year = 2010,
                    Country = country5,
                    Genres = new List<Genre>
                        { genre1, genre3, genre4, genre7, genre8 },
                    Actors = new List<Actor>
                        { actor1, actor3, actor5, actor7, actor11, actor13, actor15 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 5,
                    Name = "Зеленая миля",
                    Year = 1999,
                    Country = country1,
                    Genres = new List<Genre>
                        { genre2, genre5, genre6, genre8, genre9 },
                    Actors = new List<Actor>
                        { actor2, actor4, actor6, actor8, actor9, actor10, actor12, actor14 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 6,
                    Name = "Вечное сияние чистого разума",
                    Year = 2004,
                    Country = country2,
                    Genres = new List<Genre>
                        { genre1, genre3, genre4, genre7, genre8 },
                    Actors = new List<Actor>
                        { actor1, actor3, actor5, actor7, actor11, actor13, actor15 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 7,
                    Name = "Крестный отец",
                    Year = 1972,
                    Country = country3,
                    Genres = new List<Genre>
                        { genre2, genre5, genre6, genre8, genre9 },
                    Actors = new List<Actor>
                        { actor2, actor4, actor6, actor8, actor9, actor10, actor12, actor14 }
                });

                db.Movies.Add(new Movie()
                {
                    Id = 8,
                    Name = "Сияние",
                    Year = 1980,
                    Country = country4,
                    Genres = new List<Genre>
                        { genre1, genre3, genre4, genre7, genre8 },
                    Actors = new List<Actor>
                        { actor1, actor3, actor5, actor7, actor11, actor13, actor15 }
                });
                */

                base.Seed(db);
            }
        }

    }
}