using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieSearch.Models
{
    public class Country
    {
        public Country()
        {
            Countries = new List<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // Коллекция Movie связанных с текущим объектом Country (один ко многим)
        public virtual ICollection<Movie> Countries { get; set; }
    }
}