using Application.Bussiness;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using viska_kino_mvc.Models;
using Data_Access.Models;

namespace viska_kino_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScreeningService _screeningService;

        private readonly ILogger<HomeController> _logger;

        private readonly ILoginService _loginService;
        private readonly IReservationService _reservationService;

        public HomeController(ILogger<HomeController> logger, IScreeningService screeningService,ILoginService loginService,IReservationService reservationService)
        {
            _logger = logger;
            _screeningService = screeningService;
            _loginService = loginService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int screeningId)
        {
            var screening = await _screeningService.GetScreeningByIdAsync(screeningId);
            if (screening == null)
            {
                return NotFound(); 
            }

            var model = new CreateReservationViewModel
            {
                ScreeningId = screening.Id,
                FilmTitle = screening.Film.Title,
                FilmId = screening.FilmId,
                ScreeningTime = screening.ScreeningTime,
                ScreeningRoomId = screening.ScreeningRoomId,
                ScreeningRoomName = screening.ScreeningRoom.Name
            };
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel model)
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            User user = _loginService.GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login");
            }   

            Console.WriteLine("Making reservation");
            Console.WriteLine(user.Id);

            await _reservationService.MakeReservation(new Data_Access.Models.Reservation { UserId = _loginService.GetCurrentUser().Id, ScreeningId = model.ScreeningId, NumberOfSeats = model.NumberOfSeats });

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            Console.WriteLine(_loginService.IsLoggedIn());
            var today = DateTime.Today; 
            Console.WriteLine(today);
            var screenings = await _screeningService.GetScreeningsByDateAsync(today);

            
            return View(screenings);
        }

        public IActionResult Privacy()
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            return View();

        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (_loginService.Login(login.Username, login.Password).Result)
            {

                ViewBag.IsLoggedIn = true;
                Console.WriteLine("Logged in");
                Console.WriteLine(_loginService.IsLoggedIn());
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Privacy");
            }
            
        }
        public IActionResult Logout()
        {
            _loginService.Logout();
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            ViewBag.IsLoggedIn = _loginService.IsLoggedIn();
            return View();
        }
    }
}
