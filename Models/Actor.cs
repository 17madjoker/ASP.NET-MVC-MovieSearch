using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSearch.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // Коллекция Movie связанных с текущим объектом Actor (многие ко многим)
        public virtual ICollection<Movie> Movies { get; set; }
    }
}