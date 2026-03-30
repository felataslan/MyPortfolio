using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class SocialMediaController : Controller
    {
        private readonly MyPortfolioContext _context;

        public SocialMediaController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SocialMediaList()
        {
            var values = _context.SocialMedias.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia entity)
        {
            _context.SocialMedias.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("SocialMediaList");
        }

        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if(value != null) {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("SocialMediaList");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMedia entity)
        {
            _context.SocialMedias.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("SocialMediaList");
        }
    }
}
