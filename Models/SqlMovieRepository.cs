using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class SqlMovieRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;
        public SqlMovieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Movie Add(Movie movie)
        {
            //movie.ID = Guid.NewGuid().GetHashCode();
            _appDbContext.Add(movie);
            _appDbContext.SaveChanges();
            return movie;
        }

        public Movie Delete(int id)
        {
            var _movie = _appDbContext.Movies.Find(id);
            if (_movie!=null)
            {
                _appDbContext.Movies.Remove(_movie);
                _appDbContext.SaveChanges();
            }
            return _movie;
        }

        public Movie Edit(Movie moviechanges)
        {
            var _movie = _appDbContext.Movies.Attach(moviechanges);
            if (_movie != null)
            {
                _movie.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _appDbContext.SaveChanges();
            }
            return moviechanges;
        }

        List<Movie> IMovieRepository.GetAllMovies()
        {
            return _appDbContext.Movies.ToList();
        }

        Movie IMovieRepository.GetMovie(int id)
        {
            return _appDbContext.Movies.FirstOrDefault(m => m.ID == id);
        }
    }
}
