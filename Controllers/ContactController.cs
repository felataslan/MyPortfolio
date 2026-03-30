using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly MyPortfolioContext _context;

        public ContactController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ContactList()
        {
            var values = _context.Contacts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateContact(Contact entity)
        {
            _context.Contacts.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("ContactList");
        }

        public IActionResult DeleteContact(int id)
        {
            var value = _context.Contacts.Find(id);
            if(value != null) {
                _context.Contacts.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value = _context.Contacts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact entity)
        {
            _context.Contacts.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("ContactList");
        }
    }
}
