using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Controllers
{
    [AuthorizeAttribute_64130299Controller]
    public class Movie_64130299Controller : Controller
    {
        private MovieDatabase_64130299Entities db = new MovieDatabase_64130299Entities();
        // Utility method to generate MovieId
        private string GenerateMovieId(string title)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(title));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString().Substring(0, 10); // Use first 10 characters of the hash
            }
        }
        // GET: Movie_64130299
        public ActionResult Index(
    string titleFilter = "",
    bool? genreIdFilter = null,
    DateTime? createdAtFrom = null,
    DateTime? createdAtTo = null,
    string genreNameFilter = "",
    string directorNameFilter = "",
    DateTime? updatedAtFrom = null,
    DateTime? updatedAtTo = null,
    string countryFilter = "",
    string actorNameFilter = "",
    string descriptionFilter = "",
    DateTime? releaseDateFilter = null
)
        {
            // Start with the full movie list
            var movies = db.Movie_64130299.AsQueryable();

            // Apply each filter if it has a value
            if (!string.IsNullOrEmpty(titleFilter))
                movies = movies.Where(m => m.Title.Contains(titleFilter));

            if (genreIdFilter.HasValue)
                movies = movies.Where(m => m.GenreId == genreIdFilter);

            if (createdAtFrom.HasValue)
                movies = movies.Where(m => m.CreatedAt >= createdAtFrom);

            if (createdAtTo.HasValue)
                movies = movies.Where(m => m.CreatedAt <= createdAtTo);

            if (!string.IsNullOrEmpty(genreNameFilter))
                movies = movies.Where(m => m.GenreName.Contains(genreNameFilter));

            if (!string.IsNullOrEmpty(directorNameFilter))
                movies = movies.Where(m => m.DirectorName.Contains(directorNameFilter));

            if (updatedAtFrom.HasValue)
                movies = movies.Where(m => m.UpdatedAt >= updatedAtFrom);

            if (updatedAtTo.HasValue)
                movies = movies.Where(m => m.UpdatedAt <= updatedAtTo);

            if (!string.IsNullOrEmpty(countryFilter))
                movies = movies.Where(m => m.Country.Contains(countryFilter));

            if (!string.IsNullOrEmpty(actorNameFilter))
                movies = movies.Where(m => m.ActorName.Contains(actorNameFilter));

            if (!string.IsNullOrEmpty(descriptionFilter))
                movies = movies.Where(m => m.Description.Contains(descriptionFilter));

            if (releaseDateFilter.HasValue)
                movies = movies.Where(m => m.ReleaseDate == releaseDateFilter);

            // Return the filtered result to the view
            return View("Index",movies.ToList());
        }


        // GET: Movie_64130299/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        // GET: Movie_64130299/Create
        public ActionResult Create()
        {
            var movie = new Movie_64130299
            {
                MovieId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            return View(movie);
        }

        // POST: Movie_64130299/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,GenreId,GenreName,DirectorName,ActorName,Country,ReleaseDate,PosterUrl,TrailerUrl,CreatedAt,UpdatedAt")] Movie_64130299 movie_64130299)
        {
            if (ModelState.IsValid)
            {
                // Generate a unique MovieId based on the Title
                movie_64130299.MovieId = Guid.NewGuid().ToString();
                movie_64130299.CreatedAt = DateTime.Now;
                movie_64130299.UpdatedAt = DateTime.Now;

                db.Movie_64130299.Add(movie_64130299);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie_64130299);
        }

        // GET: Movie_64130299/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        // POST: Movie_64130299/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,Description,GenreId,GenreName,DirectorName,ActorName,Country,ReleaseDate,PosterUrl,TrailerUrl,CreatedAt,UpdatedAt")] Movie_64130299 movie_64130299)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie_64130299).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie_64130299);
        }

        // GET: Movie_64130299/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            if (movie_64130299 == null)
            {
                return HttpNotFound();
            }
            return View(movie_64130299);
        }

        // POST: Movie_64130299/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Movie_64130299 movie_64130299 = db.Movie_64130299.Find(id);
            db.Movie_64130299.Remove(movie_64130299);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
