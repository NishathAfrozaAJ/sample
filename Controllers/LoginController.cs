using Microsoft.AspNetCore.Mvc;
using Project3.Models;

namespace Project3.Controllers
{
    public class LoginController : Controller
    {
        public readonly stockContext db;
        public readonly ISession session;
        public LoginController(stockContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Stockist p)
        {
            var res = (from i in db.Stockists
                       where (i.StName == p.StName) && (i.Password == p.Password)
                       select i).SingleOrDefault();
            if (res != null)
            {
                session.SetString("StName", res.StName);
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = "Invalid Username";
                return View();
            }
        }
        public IActionResult Index()
        {
            ViewBag.name = HttpContext.Session.GetString("StName");
            return View();
        }
        public IActionResult cview()
        {
            return View();
        }
        [HttpPost]
        public IActionResult cview(Stockist p)
        {
            var res = (from i in db.Stockists
                       where (i.StName == p.StName) && (i.Password == p.Password)
                       select i).SingleOrDefault();
            if (res != null)
            {
                session.SetString("StName", res.StName);
                return RedirectToAction("Index", "Customer");

            }
            else
            {
                ViewBag.Message = "Invalid Username";
                return View();
            }
            
        }
        public IActionResult Logout() 
        {
            session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
    }
    
    }

