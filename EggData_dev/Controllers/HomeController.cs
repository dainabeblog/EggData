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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
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

        public async Task<IActionResult> Index()
        {
            var product_store = new Product_StoreViewModel();

            //store 一覧
            product_store.Store = await _context.Store
               .Where(e => e.UserName == User.Identity.Name)
               .OrderBy(Store => Store.StoreId)
               .ToListAsync();
            List<SelectListItem> SLIstore = new List<SelectListItem>();
            foreach(var item in product_store.Store)
            {
                SLIstore.Add(new SelectListItem { Text = item.Name, Value = item.StoreId.ToString() });
            }
            ViewBag.SLIstore_VB = SLIstore;

            //商品一覧
            product_store.Product = await _context.Product
                .Where(e => e.UserName == User.Identity.Name)
                .OrderBy(Product => Product.ProductId)
                .ToListAsync();
            List<SelectListItem> SLIproduct = new List<SelectListItem>();
            foreach (var item in product_store.Product)
            {
                SLIproduct.Add(new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() });
            }
            ViewBag.products_VB = product_store.Product;
            ViewBag.SLIproduct_VB = SLIproduct;
            Debug.WriteLine("商品の数:" + product_store.Product.Count);

            // SalesDate一覧
            DateTime dt_now = DateTime.Now;
            product_store.SalesData = await _context.SalesData
                //.Where(e => e.date.ToString("yyyy-MM-dd") == dt.ToString("yyyy-MM-dd"))→これはQiitaの記事にする
               .Where(e => e.UserName == User.Identity.Name
                        && e.date.Year == dt_now.Year
                        && e.date.Month == dt_now.Month
                        && e.date.Day == dt_now.Day)
               .OrderBy(SalesData => SalesData.StoreId)
               .ThenBy(SalesData => SalesData.ProductId)
               .ToListAsync();
            ViewBag.SalesDatas_VB = product_store.SalesData;


            object[,] a2Ds_salesDate = new object[SLIstore.Count, SLIproduct.Count + 1];
            for(var i = 0; i < SLIstore.Count; i++)
            {
                for(var j = 0; j <= SLIproduct.Count; j++)
                {
                    if(i == 0 && j == 0)
                    {
                        Debug.WriteLine("------表の出力start--------");
                    }


                    if (j == 0)
                    {
                            a2Ds_salesDate[i, j] = SLIstore[i];
                    }
                    else
                    {
                        if( product_store.SalesData.Any(m => m.Store.Name == SLIstore[i].Text && m.Product.Name == SLIproduct[j - 1].Text) )
                        {
                            a2Ds_salesDate[i, j] = product_store.SalesData.Find(m => m.Store.Name == SLIstore[i].Text && m.Product.Name == SLIproduct[j - 1].Text);
                        }
                        else
                        {

                            a2Ds_salesDate[i, j] = "0";
                        }
                    }


                    if( j == SLIproduct.Count)
                    {
                        Debug.WriteLine(a2Ds_salesDate[i, j]);
                    }
                    else
                    {
                        Debug.Write(a2Ds_salesDate[i, j] + ",");
                    }
                }
                if (i == SLIstore.Count - 1)
                {
                    Debug.WriteLine("------表の出力end--------");
                }
            }
            ViewBag.a2dSalesData = a2Ds_salesDate;

            return View();
        }

        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("date,StoreId,ProductId,saledCount,UserName")] SalesData salesData)
        {
            var product_store = new Product_StoreViewModel();

            //store 一覧
            product_store.Store = await _context.Store
               .Where(e => e.UserName == User.Identity.Name)
               .ToListAsync();
            List<SelectListItem> SLIstore = new List<SelectListItem>();
            foreach (var item in product_store.Store)
            {
                SLIstore.Add(new SelectListItem { Text = item.Name, Value = item.StoreId.ToString() });
            }
            ViewBag.SLIstore_VB = SLIstore;

            //商品一覧
            product_store.Product = await _context.Product
                .Where(e => e.UserName == User.Identity.Name)
                .ToListAsync();
            List<SelectListItem> SLIproduct = new List<SelectListItem>();
            foreach (var item in product_store.Product)
            {
                SLIproduct.Add(new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() });
            }
            ViewBag.products_VB = product_store.Product;
            ViewBag.SLIproduct_VB = SLIproduct;

            //salesData一覧
            DateTime dt_now = DateTime.Now;
            product_store.SalesData = await _context.SalesData
               .Where(e => e.UserName == User.Identity.Name
                        && e.date.Year == dt_now.Year
                        && e.date.Month == dt_now.Month
                        && e.date.Day == dt_now.Day)
               .OrderBy(SalesData => SalesData.StoreId)
               .ThenBy(SalesData => SalesData.ProductId)
               .ToListAsync();
            object[,] a2Ds_salesDate = new object[SLIstore.Count, SLIproduct.Count + 1];
            for (var i = 0; i < SLIstore.Count; i++)
            {
                for (var j = 0; j <= SLIproduct.Count; j++)
                {
                    if (j == 0)
                    {
                        a2Ds_salesDate[i, j] = SLIstore[i];
                    }
                    else
                    {
                        if (product_store.SalesData.Any(m => m.Store.Name == SLIstore[i].Text && m.Product.Name == SLIproduct[j - 1].Text))
                        {
                            a2Ds_salesDate[i, j] = product_store.SalesData.Find(m => m.Store.Name == SLIstore[i].Text && m.Product.Name == SLIproduct[j - 1].Text);
                        }
                        else
                        {
                            a2Ds_salesDate[i, j] = "0";
                        }
                    }
                }
            }
            ViewBag.a2dSalesData = a2Ds_salesDate;


            // SalesDate一覧
            if (ModelState.IsValid)
            {
                _context.Add(salesData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            //return RedirectToAction("Index", "Home");
            //Redirectにしたら検証できない→いずれ解決したい。z
            return View(salesData);
        }


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
