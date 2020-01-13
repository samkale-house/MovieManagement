using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManagement.Models;
using MovieManagement.ViewModels;

namespace MovieManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository,IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        

        public ViewResult Index()
        {
            var model = _movieRepository.GetAllMovies();
            return View(model);
        }

        public ViewResult Privacy()
        {
            return View();
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel movieVM)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName=null;
                if (movieVM.PhotoPath != null)
                {
                    //copy the image from selected location to images folder on server
                    string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName= Guid.NewGuid().ToString() + "_" + movieVM.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadfolder, uniqueFileName);
                    movieVM.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                 }

                Movie newMovie = new Movie
                {
                    Title = movieVM.Title,
                    Rating = movieVM.Rating,
                    Genre = movieVM.Genre,
                    Price = movieVM.Price,
                    ReleaseDate=movieVM.ReleaseDate,
                    PhotoPath = uniqueFileName
                };
               newMovie=_movieRepository.Add(newMovie);
               return RedirectToAction("details", new { id = newMovie.ID });
            }
            return View();
        }
        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsVM = new HomeDetailsViewModel()
            {
                Movie = _movieRepository.GetMovie(id),
                PageTitle = "Movie Details"
            };
            return View(homeDetailsVM);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
