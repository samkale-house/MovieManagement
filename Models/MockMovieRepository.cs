using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MockMovieRepository:IMovieRepository
    {
        private List<Movie> _movieList;
        public MockMovieRepository()
        {
            _movieList = new List<Movie>()
            {
                new Movie(){ID=1 ,Price=23,Genre=Generes.Action,Title="Star Wars",Rating=3,ReleaseDate=DateTime.Today}
            };
        }
        
        public List<Movie> GetAllMovies()
        {
            return _movieList;
        }

        public Movie GetMovie(int id)
        { return _movieList.FirstOrDefault(m => m.ID == id); }

        public Movie Add(Movie movie)
        {
            movie.ID = _movieList.Max(m => m.ID) + 1;
            _movieList.Add(movie);
            return movie;
        }
        
        public Movie Delete(int id)
        {
            var _movie = _movieList.Find(m => m.ID == id);
            if(_movie!=null)
            { _movieList.Remove(_movie); }
            return _movie;
        }

        public Movie Edit(Movie movie)
        {
            var _movie = _movieList.Find(m => m.ID == movie.ID);
            if (_movie != null)
            {
                _movie.Title = movie.Title;
                _movie.Genre = movie.Genre;
                _movie.Rating = movie.Rating;
                _movie.ReleaseDate = movie.ReleaseDate;
                _movie.Price = movie.Price;
            }
            return _movie;
        }
           
    }       
}
