using MovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;

namespace MovieSearch.Controllers
{
    public class HomeController : Controller
    {
        MovieDbContext db = new MovieDbContext();

        // searchBy - параметр выборки для аттрибутов ссылок пагинации
        public ActionResult Index(int? page,
            string searchBy = "byName", string searchText = "", string bySelectCountry = "", string bySelectGenre = "")
        {
            ViewBag.Genres = new SelectList(db.Genres, "Id", "Name");
            ViewBag.Countries = new SelectList(db.Countries, "Id", "Name");

            ViewBag.searchBy = searchBy;
            ViewBag.searchText = searchText;
            ViewBag.bySelectCountry = bySelectCountry;
            ViewBag.bySelectGenre = bySelectGenre;

            // if page == null then page = 1
            return View(db.Movies.ToList().ToPagedList(page ?? 1, 3));
        }

        [HttpPost]
        public PartialViewResult GetActorsByMovie(int movieId)
        {
            return PartialView("_GetActorsByMovie", db.Movies.Find(movieId));
        }

        [HttpPost]
        public PartialViewResult GetMoviesBySearch(int? page,
            string searchBy = "", string searchText = "", string bySelectCountry = "", string bySelectGenre = "")
        {
            List<Movie> model = new List<Movie>();
            int? countryId = null;
            int? genreId = null;

            ViewBag.searchBy = searchBy;
            ViewBag.searchText = searchText;
            ViewBag.bySelectCountry = bySelectCountry;
            ViewBag.bySelectGenre = bySelectGenre;

            if (!string.IsNullOrEmpty(bySelectCountry))
                countryId = Convert.ToInt32(bySelectCountry);

            if (!string.IsNullOrEmpty(bySelectGenre))
                genreId = Convert.ToInt32(bySelectGenre);

            switch (searchBy)
            {
                case "byName" :
                    model = db.Movies.Where(m => m.Name.Contains(searchText) && 
                        (countryId == null ? true : m.Country.Id == countryId)  &&
                        (genreId == null ? true : m.Genres.Any(a => a.Id == genreId))).ToList();
                    break;

                case "byActor":
                    model = db.Movies.Where(m => m.Actors.Any(a => a.Name.Contains(searchText)) &&
                        (countryId == null ? true : m.Country.Id == countryId) &&
                         (genreId == null ? true : m.Genres.Any(a => a.Id == genreId))).ToList();
                    break;
            }

            return PartialView("_GetMovies", model.ToPagedList(page ?? 1, 3));
        }

        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(db.Countries, "Id", "Name");
            ViewBag.Genres = new SelectList(db.Genres, "Id", "Name"); 
            ViewBag.Actors = new SelectList(db.Actors, "Id", "Name"); 

            return View("CreateEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, Year")] Movie movie, int Countries, int[] Genres, int[] Actors)
        {
            if (ModelState.IsValid)
            {
                Movie newMovie = new Movie();

                newMovie.Name = movie.Name;
                newMovie.Year = movie.Year;
                newMovie.Country = db.Countries.Find(Countries);

                for (int i = 0; i < Genres.Length; i++)
                    newMovie.Genres.Add(db.Genres.Find(Genres[i]));

                for (int j = 0; j < Actors.Length; j++)
                    newMovie.Actors.Add(db.Actors.Find(Actors[j]));

                db.Movies.Add(newMovie);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("CreateEdit", movie);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = db.Movies.Find(id);

            if (movie == null)
                return HttpNotFound();

            ViewBag.Countries = new SelectList(db.Countries, "Id", "Name", movie.Country.Id);
            ViewBag.Genres = new MultiSelectList(db.Genres, "Id", "Name", movie.Genres.Select(g => g.Id));
            ViewBag.Actors = new MultiSelectList(db.Actors, "Id", "Name", movie.Actors.Select(g => g.Id));

            return View("CreateEdit", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Year")] Movie movie, int Countries, int[] Genres, int[] Actors)
        {
            if (ModelState.IsValid)
            {                           
                movie.Country = db.Countries.Find(Countries);

                movie.Genres.Clear();
                movie.Actors.Clear();

                for (int i = 0; i < Genres.Length; i++)
                    movie.Genres.Add(db.Genres.Find(Actors[i]));
                
                for (int j = 0; j < Actors.Length; j++)
                    movie.Actors.Add(db.Actors.Find(Actors[j]));

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
                
            }

            return View("CreateEdit", movie);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = db.Movies.Find(id);

            if (movie == null)
                return HttpNotFound();

            return PartialView("_Delete", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);

            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();

                return true;
            }

            return false;
        }














        /*
        public string TestDB()
        {
            List<Genre> data;
            string res = "";

            using (MovieDbContext db = new MovieDbContext())
            {
                data = db.Movies.Find(2).Genres.ToList();
            }

            foreach (Genre item in data)
                res += $"{item.Name}<hr>";

            return res;
        }*/
    }
}