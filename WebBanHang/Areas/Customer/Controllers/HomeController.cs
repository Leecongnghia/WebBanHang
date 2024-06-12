using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        //Hiển thị danh Sách all product
        //Phân trang
        public IActionResult Index(int? page)
        {
            var pageIndex = (int)(page != null ? page : 1);
            var pageSize = 5;
            var dsSanPham = _db.Products.ToList();

            //Thống kê số trang
            var pageSum = dsSanPham.Count() / pageSize + (dsSanPham.Count() % pageSize > 0 ? 1 : 0);
            //Truyền dữ liệu cho view
            ViewBag.PageSum = pageSum;
            ViewBag.PageIndex = pageIndex;
            var productlist = _db.Products.ToList();
            return View(productlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
