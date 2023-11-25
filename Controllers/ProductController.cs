using Lab_WT_Data.Data;
using Microsoft.AspNetCore.Mvc;
using Lab_WT_Data.Entities;
using AspNet_Projects.Models;
using AspNet_Projects.Extensions;
using Microsoft.AspNetCore.Http;


namespace AspNet_Projects.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        //private ILogger _logger;

        int _pageSize;

        public ProductController(ApplicationDbContext context/*, ILogger<ProductController> logger*/)
        {
            _pageSize = 3;
            _context = context;
            //_logger = logger;
        }
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {

            var dishesFiltered = _context.Dishes.Where(d => !group.HasValue || d.DishGroupId == group.Value);
            ViewData["Groups"] = _context.Groups;
            ViewData["CurrentGroup"] = group ?? 0;
            var model = ListViewModel<Dish>.GetModel(dishesFiltered, pageNo, _pageSize);
            //_logger.LogInformation($"info: group={group}, page={pageNo}");
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
    }
}