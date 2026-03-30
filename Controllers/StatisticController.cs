using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
    [Authorize]
	public class StatisticController : Controller
    {
        private readonly MyPortfolioContext context;
        public StatisticController(MyPortfolioContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            ViewBag.v1 = context.Skills.Count();
            ViewBag.v2 = context.Messages.Count();
            ViewBag.v3 = context.Messages.Where(x => x.IsRead == false).Count();
            ViewBag.v4 = context.Messages.Where(x => x.IsRead == true).Count();
            return View();
        }
    }
}
