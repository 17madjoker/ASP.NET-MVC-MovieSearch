using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSearch.Models
{
    public class Genre
    {
        public Genre()
        {
            Genres = new List<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // Коллекция Movie связанных с текущим объектом Genre (многие ко многим)
        public virtual ICollection<Movie> Genres { get; set; }
    }
}