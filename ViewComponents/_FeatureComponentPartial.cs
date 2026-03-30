using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _FeatureComponentPartial:ViewComponent
    {
        private readonly MyPortfolioContext portfolioContext;
        public _FeatureComponentPartial(MyPortfolioContext _context)
        {
            portfolioContext = _context;
        }
        public IViewComponentResult Invoke()
        {
            var values = portfolioContext.Features.ToList();
            return View(values);
        }
    }
}
