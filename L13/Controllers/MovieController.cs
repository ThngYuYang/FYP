using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using L13.Models;

namespace L13.Controllers
{
   public class MovieController : Controller
   {
      // TODO: L13 Task 01a - Use [AllowAnonymous] or [Authorise] to specify access control
      [AllowAnonymous]
      public IActionResult About()
      {
         return View();
      }
      [Authorize(Roles = "manager, member")]
      public IActionResult ListMovies()
      {

         // Get a list of all movies from the database
         List<Movie> movie = DBUtl.GetList<Movie>(
               @"SELECT * FROM Movie, Genre
                  WHERE Movie.GenreId = Genre.GenreId");
         return View(movie);

      }
      [Authorize(Roles = "manager")]
      public IActionResult AddMovie()
      {
         ViewData["Genres"] = GetListGenres();
         return View();
      }

      [HttpPost]
      public IActionResult AddMovie(Movie newMovie)
      {
         if (!ModelState.IsValid)
         {
            ViewData["Genres"] = GetListGenres();
            ViewData["Message"] = "Invalid Input";
            ViewData["MsgType"] = "warning";
            return View("AddMovie");
         }
         else
         {
            // TODO: L13 Task 10 - Secure INSERT statement
            string insert =
               @"INSERT INTO Movie(Title, ReleaseDate, Price, Duration, Rating, GenreId) 
                 VALUES('{0}', '{1:yyyy-MM-dd}', {2}, {3}, '{4}', {5})";
            int result = DBUtl.ExecSQL(insert, newMovie.Title, newMovie.ReleaseDate, newMovie.Price,
                                     newMovie.Duration, newMovie.Rating, newMovie.GenreId);

            if (result == 1)
            {
               TempData["Message"] = "Movie Created";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
            return RedirectToAction("ListMovies");
         }
      }

      [HttpGet]
      [Authorize(Roles = "manager")]
      public IActionResult EditMovie(string id)
      {
         // TODO: L13 Task 08 - Show form with values for update
         // To start this TASK, delete 3 lines in this method with "REMOVE THIS LINE"
         
          
         // Get the record from the database using the id
         string movieSql = @"SELECT MovieId, Title, ReleaseDate, Price, 
                                    Rating, Duration, Genre.GenreId
                               FROM Movie, Genre
                              WHERE Movie.GenreId = Genre.GenreId
                                AND Movie.MovieId = '{0}'";
         List<Movie> lstMovie = DBUtl.GetList<Movie>(movieSql, id);

         // If the record is found, pass the model to the View
         if (lstMovie.Count == 1)
         {
            ViewData["Genres"] = GetListGenres();
            return View(lstMovie[0]);
         }
         else
         // Otherwise redirect to the movie list page
         {
            TempData["Message"] = "Movie not found.";
            TempData["MsgType"] = "warning";
            return RedirectToAction("ListMovies");
         }
      }

      [HttpPost]
      [Authorize(Roles = "manager")]
      public IActionResult EditMovie(Movie movie)
      {
         // TODO: L13 Task 09 - Update database table 
         // Check the state of the model ((Ref Week 9). 
          if (!ModelState.IsValid)
            {
                ViewData["Genres"] = GetListGenres();
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("EditMovie");
            }
            // Write the SQL statement
            string update = @"UPDATE Movie SET Title='{1}', ReleaseDate='{2:yyyy-MM-dd}', 
                               Price= {3}, Duration={4}, Rating='{5}', GenreId={6}
                                WHERE MovieId={0}";
            // Execute the SQL statement in a secure manner
            int result = DBUtl.ExecSQL(update, movie.MovieId,
                movie.Title, movie.ReleaseDate, movie.Price, movie.Duration,
                movie.Rating, movie.GenreId);
            // Check the result and branch
            // If successful set a TempData success Message and MsgType
            if (result == 1)
            {
                TempData["Message"] = "Movie Updated";
                TempData["MsgType"] = "success";
            }
            // If unsuccessful, set a TempData message that equals the DBUtl error message
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["Msgtype"] = "danger";
            }
            // Call the action ListMovies to show the result of the update
            return RedirectToAction("ListMovies");
      }

      [Authorize(Roles = "manager")]
      public IActionResult DeleteMovie(int id)
      {
         string select = @"SELECT * FROM Movie 
                              WHERE MovieId={0}";
         DataTable ds = DBUtl.GetTable(select, id);
         if (ds.Rows.Count != 1)
         {
            TempData["Message"] = "Movie record no longer exists.";
            TempData["MsgType"] = "warning";
         }
         else
         {
            string delete = "DELETE FROM Movie WHERE MovieId={0}";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
               TempData["Message"] = "Movie Deleted";
               TempData["MsgType"] = "success";
            }
            else
            {
               TempData["Message"] = DBUtl.DB_Message;
               TempData["MsgType"] = "danger";
            }
         }
         return RedirectToAction("ListMovies");
      }

      [Authorize(Roles = "manager")]
      private List<Genre> GetListGenres()
      {
         // Get a list of all genres from the database
         string genreSql = @"SELECT GenreId, GenreName 
                                FROM Genre";
         List<Genre> lstGenre = DBUtl.GetList<Genre>(genreSql);
         return lstGenre;
      }
   }
}
//20031509 Thng Yu Yang