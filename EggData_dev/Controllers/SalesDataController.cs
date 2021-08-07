using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EggData_dev.Data;
using EggData_dev.Models;
using System.Diagnostics;

namespace EggData_dev.Controllers
{
    public class SalesDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesData.ToListAsync());
        }


        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("date,StoreId,ProductId,saledCount,UserName")] SalesData salesData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Setting", "Home");
            }
            return View(salesData);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Debug.WriteLine("ここを通っている！！！！にょ〜〜〜〜");
            var product_store = new Product_StoreViewModel();

            //store 一覧
            product_store.Store = await _context.Store
               .Where(e => e.UserName == User.Identity.Name)
               .OrderBy(Store => Store.StoreId)
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
                .OrderBy(Product => Product.ProductId)
                .ToListAsync();
            List<SelectListItem> SLIproduct = new List<SelectListItem>();
            foreach (var item in product_store.Product)
            {
                SLIproduct.Add(new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() });
            }
            ViewBag.products_VB = product_store.Product;
            ViewBag.SLIproduct_VB = SLIproduct;
            if (id == null)
            {
                return NotFound();
            }

            var salesData = await _context.SalesData.FindAsync(id);
            if (salesData == null)
            {
                return NotFound();
            }
            return View(salesData);
        }


        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesDataId,date,StoreId,ProductId,saledCount,UserName")] SalesData salesData)
        {
            Debug.WriteLine("ここを通っている！！！！にょーーここでエラーが出ている");
            var product_store = new Product_StoreViewModel();

            //store 一覧
            product_store.Store = await _context.Store
               .Where(e => e.UserName == User.Identity.Name)
               .OrderBy(Store => Store.StoreId)
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
                .OrderBy(Product => Product.ProductId)
                .ToListAsync();
            List<SelectListItem> SLIproduct = new List<SelectListItem>();
            foreach (var item in product_store.Product)
            {
                SLIproduct.Add(new SelectListItem { Text = item.Name, Value = item.ProductId.ToString() });
            }
            ViewBag.products_VB = product_store.Product;
            ViewBag.SLIproduct_VB = SLIproduct;

            if (id != salesData.SalesDataId)
            {
                Debug.WriteLine("ここを通っている！！！！???");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesDataExists(salesData.SalesDataId))
                    {
                        Debug.WriteLine("ここを通っている！！！！");
                        Console.WriteLine("aaaa");
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine("ここを通っている！！！！???!!!!");
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(salesData);
        }

        //// GET: Product/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var salesData = await _context.SalesData
        //        .FirstOrDefaultAsync(m => m.SalesDataId == id);
        //    if (salesData == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(salesData);
        //}

        //// POST: Product/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var salesData = await _context.SalesData.FindAsync(id);
        //    _context.SalesData.Remove(salesData);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        private bool SalesDataExists(int id)
        {
            return _context.SalesData.Any(e => e.SalesDataId == id);
        }
    }
}
