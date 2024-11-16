using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_Ban_Quan_Ao.Models.ModelsView;

namespace Web_Ban_Quan_Ao.Controllers
{
    public class CartsController : Controller
    {
        private const string CartSessionKey = "Cart";

        // Lấy giỏ hàng từ session
        private List<Carts> GetCart()
        {
            var cart = Session[CartSessionKey] as List<Carts>;
            if (cart == null)
            {
                cart = new List<Carts>();
                Session[CartSessionKey] = cart;
            }
            return cart;
        }

        // Thêm sản phẩm vào giỏ hàng
        public ActionResult AddToCart(int id, string name, string image, float price)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                item.Qty++; // Tăng số lượng nếu sản phẩm đã tồn tại
            }
            else
            {
                cart.Add(new Carts
                {
                    Id = id,
                    Name = name,
                    Image = image,
                    Price = price,
                    Qty = 1
                });
            }
            Session[CartSessionKey] = cart; // Cập nhật lại session
            return RedirectToAction("Index", "Carts"); // Sau khi thêm sản phẩm, chuyển hướng tới giỏ hàng
        }

        // Xem giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            ViewBag.TotalPrice = cart.Sum(c => c.Total); // Tính tổng tiền giỏ hàng
            return View(cart);
        }

        // Cập nhật giỏ hàng
        [HttpPost]
        public ActionResult UpdateCart(int id, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Qty = quantity;
                }
                else
                {
                    cart.Remove(item); // Xóa sản phẩm nếu số lượng là 0
                }
            }
            Session[CartSessionKey] = cart;
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            Session[CartSessionKey] = cart;
            return RedirectToAction("Index");
        }

        // Thanh toán
        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.Count == 0)
            {
                return RedirectToAction("Index"); // Nếu giỏ hàng trống, chuyển về trang giỏ hàng
            }

            // Xử lý thanh toán ở đây (lưu đơn hàng vào database, gửi email, v.v.)
            Session[CartSessionKey] = null; // Xóa giỏ hàng sau khi thanh toán
            return View("OrderSuccess"); // Chuyển đến trang đặt hàng thành công
        }
    }
}
