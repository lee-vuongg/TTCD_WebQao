using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CODE_WebQao.Models.ModelsView
{
    public class CartManager
    {
        private const string CartSessionKey = "CartSession";

        public static List<CartItem> GetCart(HttpContextBase context)
        {
            var cart = context.Session[CartSessionKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                context.Session[CartSessionKey] = cart;
            }
            return cart;
        }

        public static void AddToCart(HttpContextBase context, CartItem item)
        {
            var cart = GetCart(context);
            var existingItem = cart.FirstOrDefault(i => i.MaSP == item.MaSP);

            if (existingItem != null)
            {
                existingItem.So_luong += item.So_luong;
            }
            else
            {
                cart.Add(item);
            }

            context.Session[CartSessionKey] = cart;
        }

        public static void RemoveFromCart(HttpContextBase context, int maSP)
        {
            var cart = GetCart(context);
            var itemToRemove = cart.FirstOrDefault(i => i.MaSP == maSP);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            context.Session[CartSessionKey] = cart;
        }

        public static void ClearCart(HttpContextBase context)
        {
            context.Session[CartSessionKey] = new List<CartItem>();
        }

        public static decimal GetCartTotal(HttpContextBase context)
        {
            var cart = GetCart(context);
            return cart.Sum(i => i.Total);
        }
    }
}