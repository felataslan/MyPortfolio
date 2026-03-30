using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        private readonly MyPortfolioContext _context;

        public FeatureController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult FeatureList()
        {
            var values = _context.Features.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFeature(Feature entity)
        {
            _context.Features.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("FeatureList");
        }

        public IActionResult DeleteFeature(int id)
        {
            var value = _context.Features.Find(id);
            if(value != null) {
                _context.Features.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("FeatureList");
        }

        [HttpGet]
        public IActionResult UpdateFeature(int id)
        {
            var value = _context.Features.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateFeature(Feature entity)
        {
            _context.Features.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("FeatureList");
        }
    }
}
