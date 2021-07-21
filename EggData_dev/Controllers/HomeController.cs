using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EggData_dev.Models;
using EggData_dev.Data;
using Microsoft.EntityFrameworkCore;

namespace EggData_dev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        //public async Task<IActionResult> Setting()
        //{
        //    return View(await _context.Product
        //        .Where(e => e.UserName == User.Identity.Name)
        //        .ToListAsync()
        //        );
        //}
        public async Task<IActionResult> Setting()
        {
             var product_store = new Product_StoreViewModel();

            product_store.Product = await _context.Product
                .Where(e => e.UserName == User.Identity.Name)
                .ToListAsync();
            product_store.Store = await _context.Store
               .Where(e => e.UserName == User.Identity.Name)
               .ToListAsync();

            return View(product_store);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
