using Microsoft.AspNetCore.Mvc;
using Project3.Models;

namespace Project3.Controllers
{
    public class CustomerController : Controller
    {
        public readonly stockContext db;
        public readonly ISession session;
        public CustomerController(stockContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Loginc()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Loginc(Customer p)
        {
            var res = (from i in db.Customers
                       where (i.Cname == p.Cname) && (i.Pass == p.Pass)
                       select i).SingleOrDefault();
            if (res != null)
            {
                ViewBag.name= session.GetString("Cname" );
                session.SetString("Cname", res.Cname);
                session.SetInt32("Cid",res.Cid);
                return RedirectToAction("Index", "Buy");

            }
            else
            {
                ViewBag.Message = "Invalid Username";
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
            
        }
        [HttpPost]
        public IActionResult Create(Customer p)

        {
            try
            {
                var r = (from i in db.Customers
                         where (i.Cname == p.Cname) 
                         select i).SingleOrDefault();
                if ((p.Pass == p.Cpass))
                {
                    if (r == null)
                    {
                        db.Customers.Add(p);
                        db.SaveChanges();
                        return RedirectToAction("Loginc");
                    }
                    else
                    {
                        ViewBag.Message = "The Username already exists";
                        return View();

                    }
                }
                else
                {
                    ViewBag.Message = "The Passwords do not match";
                    return View();
                }
                
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }

        }



    }
}
