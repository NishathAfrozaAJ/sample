using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public class ProductController : Controller
    {
        private readonly stockContext _context;
        public readonly ISession session;
        


        public ProductController(stockContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            session = httpContextAccessor.HttpContext.Session;

        }

        // GET: Product
        

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.GetString("StName");
            ViewBag.email = HttpContext.Session.GetString("StName");
            if (ViewBag.email != null)
            {
                return View(await _context.Products.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.GetString("StName");
            ViewBag.email = HttpContext.Session.GetString("StName");
            if (ViewBag.email != null)
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .FirstOrDefaultAsync(m => m.Pid == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pid,Pname,Pcost,Pquantity")] Product product)

        {
            if (ModelState.IsValid)
            {

                HttpContext.Session.GetString("StName");
                ViewBag.email = HttpContext.Session.GetString("StName");
                if (ViewBag.email != null)
                {
                    var r = (from i in _context.Products
                             where (i.Pname == product.Pname) || (i.Pid == product.Pid)
                             select i).SingleOrDefault();
                    if (r == null)
                    {
                        _context.Add(product);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "The Product already exists";
                        return View(product);

                    }
                }

                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            HttpContext.Session.GetString("StName");
            ViewBag.email = HttpContext.Session.GetString("StName");
            if (ViewBag.email != null)
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pid,Pname,Pcost,Pquantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.GetString("StName");
                ViewBag.email = HttpContext.Session.GetString("StName");
                if (ViewBag.email != null)
                {
                    if (id != product.Pid)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(product);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ProductExists(product.Pid))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    return View(product);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return View(product);
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            HttpContext.Session.GetString("StName");
            ViewBag.email = HttpContext.Session.GetString("StName");
            if (ViewBag.email != null)
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .FirstOrDefaultAsync(m => m.Pid == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {

                HttpContext.Session.GetString("StName");
                ViewBag.email = HttpContext.Session.GetString("StName");
                if (ViewBag.email != null)
                {
                    if (_context.Products == null)
                    {
                        return Problem("Entity set 'stockContext.Products'  is null.");
                    }
                    var product = await _context.Products.FindAsync(id);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Pid == id);
        }
    }
}
