using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class TestimonialController : Controller
    {
        private readonly MyPortfolioContext _context;

        public TestimonialController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial entity)
        {
            _context.Testimonials.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if(value != null) {
                _context.Testimonials.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial entity)
        {
            _context.Testimonials.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }
    }
}
