using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helpers;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController (ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //Lấy cart từ session
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            //Gửi cart qua view thông qua viewbag
            ViewBag.CART = cart;
            return View();
        }
        public IActionResult ProcessOrder(Order order)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (ModelState.IsValid)
            {
                //B1.Lưu trữ đơn hàng

                //B1.1 Thêm Order vào CSDL
                //Khởi tạo đơn hàng
                order.OrderDate = DateTime.Now;
                order.Total = cart.Total;
                order.State = "Pending";
                //Thêm order vào CSDL
                _db.Add(order);
                _db.SaveChanges();
                //B1.2 Thêm orderDetails vào CSDL
                    foreach(var item in cart.Items)
                    {
                        var orderDetail = new OrderDetail { OrderId = order.Id, ProductId = item.Product.Id, Quantity = item.Quantity };
                        //Thêm OrderDetail vào CSDL
                        _db.OrderDetails.Add(orderDetail);
                        _db.SaveChanges();
                    }
                //B2.Xóa giỏ hàng 
                HttpContext.Session.Remove("CART");
                //Trả về view hiển thị kết quả
                return View("Result");
            }
           
            //Gửi cart qua view thông qua viewbag
            ViewBag.CART = cart;
            return RedirectToAction("Index", order);
        }

    }
}
