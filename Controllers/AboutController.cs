using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private readonly MyPortfolioContext _context;

        public AboutController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(About entity)
        {
            _context.Abouts.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            if(value != null) {
                _context.Abouts.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About entity)
        {
            _context.Abouts.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("AboutList");
        }
    }
}
