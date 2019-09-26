using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieSearch.Models
{
    public class Movie
    {
        public Movie()
        {
            Genres = new List<Genre>();
            Actors = new List<Actor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите год создания фильма")]
        [Range(1960, 2019, ErrorMessage = "Год создания фильма должен быть в диапазоне 1960 - 2019")]
        public int Year { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}