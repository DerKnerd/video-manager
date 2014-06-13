using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Data.Objects;

namespace MovieManager {
    public partial class MovieEntities {
        static MovieEntities() {
            Timer t = new Timer(100);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Start();
        }
        public bool addGenre(Genre genre) {
            if (string.IsNullOrEmpty(genre.Bild))
                genre.Bild = "Default.png";
            if (this.Genres.Any(a => a.Name == genre.Name)) {
                return false;
            }
            genre.ID = this.Genres.ToList().Count;
            this.AddToGenres(genre);
            return update();
        }

        public bool changeGenre(int id, Genre genre) {
            bool res = false;
            Genre gen = getGenreById(id);
            if (gen.Bild != genre.Bild) {
                try {
                    File.Delete(gen.Bild);
                } catch (Exception) {
                    imgToDel.Add(gen.Bild);
                }
            }
            gen.Bild = genre.Bild;
            if (string.IsNullOrEmpty(genre.Bild))
                genre.Bild = "Default.png";
            gen.Name = genre.Name;
            if (this.SaveChanges() > 0)
                res = true;
            return res;
        }

        static List<string> imgToDel = new List<string>();

        static void t_Elapsed(object sender, ElapsedEventArgs e) {
            foreach (var item in imgToDel) {
                try {
                    File.Delete(item);
                    imgToDel.Remove(item);
                } catch (Exception) {
                }
            }
        }

        public bool deleteGenre(int id) {
            bool res = false;
            Genre genre = this.getGenreById(id);
            res = this.deleteGenre(genre);
            return res;
        }
        public bool deleteGenre(Genre genre) {
            bool res = false;
            this.getGenreById(genre.ID).Movies = null;
            this.SaveChanges();
            try {
                File.Delete(genre.Bild);
            } catch (Exception) {
                imgToDel.Add(genre.Bild);
            }
            this.DeleteObject(genre);
            if (this.SaveChanges() > 0)
                res = true;
            return res;
        }

        public bool update() {
            return this.SaveChanges() > 0;
        }

        public List<Genre> getAllGenres() {
            var genres = from g in Genres
                         orderby g.Name
                         select g;
            return genres.ToList();
        }
        public List<Genre> getGenresByKeyword(string keyword) {
            if (string.IsNullOrEmpty(keyword))
                return getAllGenres();
            var res = from m in this.Genres
                      where m.Name.Contains(keyword)
                      orderby m.Name
                      select m;
            return res.ToList();
        }
    
        public Genre getGenreById(int id) {
            Genre res = new Genre();
            res = this.Genres.SingleOrDefault(item => item.ID == id);
            return res;
        }
    
        public Genre getGenreByName(string name) {
            Genre res = new Genre();
            res = this.Genres.SingleOrDefault(item => item.Name == name);
            return res;
        }

        public bool addMovie(Movie movie) {
            bool res = false;
            movie.ID = this.Movies.ToList().Count;
            if (string.IsNullOrEmpty(movie.Bild))
                movie.Bild = "Default.png";
            this.AddToMovies(movie);
            if (this.SaveChanges() > 0)
                res = true;
            return res;
        }

        public bool changeMovie(int id, Movie movie) {
            bool res = false;
            Movie mov = getMovieById(id);
            if (mov.Bild != movie.Bild) {
                try {
                    File.Delete(mov.Bild);
                } catch (Exception) {
                    imgToDel.Add(mov.Bild);
                }
            }
            mov.Bild = movie.Bild;
            if (string.IsNullOrEmpty(movie.Bild))
                movie.Bild = "Default.png";
            mov.Titel = movie.Titel;
            mov.Stichworte = movie.Stichworte;
            mov.Pfad = movie.Pfad;
            mov.Lange = movie.Lange;
            mov.Inhalt = movie.Inhalt;
            mov.Grose = movie.Grose;
            if (this.SaveChanges() > 0)
                res = true;
            Refresh(RefreshMode.ClientWins, mov);
            return res;
        }

        public bool deleteMovie(Movie movie) {
            bool res = false;
            movie.Genre = null;
            try {
                File.Delete(movie.Bild);
            } catch (Exception) {
                imgToDel.Add(movie.Bild);
            }
            this.SaveChanges();
            this.DeleteObject(movie);
            if (this.SaveChanges() > 0)
                res = true;
            return res;
        }
        public bool deleteMovie(int id) {
            bool res = false;
            Movie movie = this.getMovieById(id);
            res = this.deleteMovie(movie);
            return res;
        }

        public List<Movie> getAllMovies() {
            var movies = from m in Movies
                         orderby m.Titel
                         select m;
            return movies.ToList();
        }

        public Movie getMovieById(int id) {
            Movie res = new Movie();
            res = this.Movies.SingleOrDefault(item => item.ID == id);
            return res;
        }

        public List<Movie> getMoviesByTitel(string titel) {
            var movies = from m in Movies
                         where m.Titel == titel
                         orderby m.Titel
                         select m;
            return movies.ToList();
        }

        public List<Movie> getMoviesByGenre(int id) {
            Genre genre = this.getGenreById(id);
            return getMoviesByGenre(genre);
        }
        public List<Movie> getMoviesByGenre(string name) {
            List<Movie> res = new List<Movie>();
            Genre genre = this.getGenreByName(name);
            return getMoviesByGenre(genre);
        }
        public List<Movie> getMoviesByGenre(Genre genre) {
            var movies = from m in this.Movies
                         where m.Genre.Name == genre.Name
                         orderby m.Titel
                         select m;
            return movies.ToList();
        }

        public List<Movie> getMoviesByKeyword(string keyword) {
            if (string.IsNullOrEmpty(keyword))
                return getAllMovies();
            var res = from m in this.Movies
                      where m.Stichworte.Contains(keyword) ||
                      m.Inhalt.Contains(keyword) ||
                      m.Titel.Contains(keyword)
                      orderby m.Titel
                      select m;
            return res.ToList();
        }
    }

    public partial class Movie {
        public Movie() {
        }
    }

    public partial class Genre {
        public Genre()
            : this("") {
        }
        public Genre(string name) {
            Name = name;
        }
    }
}
