using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private readonly MyPortfolioContext _context;

        public PortfolioController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult PortfolioList()
        {
            var values = _context.Portfolios.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePortfolio(Portfolio entity)
        {
            _context.Portfolios.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("PortfolioList");
        }

        public IActionResult DeletePortfolio(int id)
        {
            var value = _context.Portfolios.Find(id);
            if(value != null) {
                _context.Portfolios.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("PortfolioList");
        }

        [HttpGet]
        public IActionResult UpdatePortfolio(int id)
        {
            var value = _context.Portfolios.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdatePortfolio(Portfolio entity)
        {
            _context.Portfolios.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("PortfolioList");
        }
    }
}
