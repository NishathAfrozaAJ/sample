using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;
using System.Data;
using System.Security.Claims;

namespace Project3.Controllers
{
    public class BuyController : Controller
    {
        public readonly stockContext db;
        public readonly ISession session;
        public BuyController(stockContext _db, IHttpContextAccessor httpContextAccessor) 
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {

            HttpContext.Session.GetString("Cname");
            ViewBag.email = HttpContext.Session.GetString("Cname");
            ViewBag.a= HttpContext.Session.GetString("StName");
            if (ViewBag.email != null)
            {
                //var a = db.Purchases.Include(i => i.PidNavigation).ToList();
                var a=db.Products.ToList();
                return View(a);
            }
            else
            {
                return RedirectToAction("Loginc", "Customer");
            }
        }
        [HttpGet]
        public IActionResult PlaceOrder(int id)
        {
            ViewBag.s = new SelectList(db.Products, "Pid", "Pname");
            var x = (from i in db.Products
                    where i.Pid == id
                    select i).SingleOrDefault();
            return View(x);
        }
        [HttpPost]
        public IActionResult PlaceOrder(int id,Product p)
        {
            //ViewBag.s = new SelectList(db.Purchases, "Pid", "Pid");
            try
            {
                Random r = new Random();

                Product x = (from i in db.Products
                             where i.Pid == id
                             select i).SingleOrDefault();
                Pur pr = new();
                pr.Puid = r.Next();
                pr.Pname = x.Pname;
                pr.Pid = x.Pid;

                if (x.Pquantity == 0)
                {
                    ViewBag.Message = "Product not available";
                    return View();
                }
                if (p.Pquantity < x.Pquantity && p.Pquantity > 0)
                {
                    x.Pquantity = x.Pquantity - p.Pquantity;
                    pr.Purquantity = p.Pquantity;
                    pr.TotalPrice = pr.Purquantity * (int)x.Pcost;
                    pr.Cid = session.GetInt32("Cid");
                    ViewBag.name = session.GetString("Cname");
                    db.Purs.Add(pr);
                    db.SaveChanges();
                    ViewBag.pur = pr;
                    return RedirectToAction("bill", pr);
                }
                else
                {
                    ViewBag.Message = "Enter purchase quantity that is lesser than the product quantity";
                    return View();

                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            
            
            }

        
        public IActionResult bill(Pur p)
        {         

            return View(p);
        }
        public IActionResult allpur()
        {
            ViewBag.name = session.GetString("Cname");
            List<Pur> a = db.Purs.Where(x => x.Cid == session.GetInt32("Cid")).ToList();
            return View(a);

        }
        public IActionResult adminpur()
        {
            
            var a = db.Purs.ToList();
            return View(a);

        }
       

    }



}

