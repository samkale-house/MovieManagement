using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        Movie GetMovie(int id);
        Movie Add(Movie movie);

        Movie Delete(int id);
        Movie Edit(Movie movie);
    }
}
