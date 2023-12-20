using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SitePustok.Contexts;

namespace SitePustok.Contollers
{
    public class ProductController : Controller
    {
        PustokDBContext _db { get; }

        public ProductController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string? q, List<int>? authorids, List<int>? catids)
        {

            ViewBag.Categories = _db.Categories.Include(c => c.Products);
            ViewBag.Author = _db.Author;
            var query = _db.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p => p.Title.Contains(q));
            }
            if (catids!=null && catids.Any())
            {
                query = query.Where(p => catids.Contains(p.CategoryId));
            }
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(string? query,List<int>? authorid, List<int>? catid)
        //{
        //    ViewBag.Categories = _db.Categories.Include(c => c.Products);
        //    ViewBag.ProductCount = await _db.Products.CountAsync();
        //    ViewBag.AuthorCount = await _db.Author.CountAsync();
        //    ViewBag.Author = _db.Author;
        //    return View();

        //}
    }
}
