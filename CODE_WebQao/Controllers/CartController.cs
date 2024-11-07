using CODE_WebQao.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CODE_WebQao.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = CartManager.GetCart(HttpContext);
            return View(cart);
        }

        public ActionResult AddToCart(int maSP, string tenSP, decimal gia, int soLuong = 1)
        {
            var item = new CartItem
            {
                MaSP = maSP,
                TenSP = tenSP,
                Gia = gia,
                So_luong = soLuong
            };

            CartManager.AddToCart(HttpContext, item);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int maSP)
        {
            CartManager.RemoveFromCart(HttpContext, maSP);

            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            CartManager.ClearCart(HttpContext);

            return RedirectToAction("Index");
        }
    }
}