using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using Web_Ban_Quan_Ao.Models; // Đảm bảo bạn đã tạo mô hình User
using System.Linq;

namespace Web_Ban_Quan_Ao.Controllers
{
    public class AccountController : Controller
    {
        // Tạo một danh sách tạm thời để lưu trữ người dùng
        // Ở thực tế, bạn nên lưu thông tin người dùng vào cơ sở dữ liệu
        private static List<User> users = new List<User>();

        // Trang đăng nhập
        public ActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Kiểm tra tên người dùng và mật khẩu (kiểm tra trong danh sách người dùng đã lưu)
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Lưu tên người dùng vào Session để sử dụng trong các lần truy cập tiếp theo
                Session["Username"] = user.Username;

                // Thiết lập cookie xác thực (dùng FormsAuthentication)
                FormsAuthentication.SetAuthCookie(username, false);

                // Chuyển hướng về trang chủ sau khi đăng nhập thành công
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Nếu đăng nhập không thành công, thông báo lỗi
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }

        // Trang đăng ký
        public ActionResult Register()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        public ActionResult Register(string username, string password, string confirmPassword)
        {
            // Kiểm tra mật khẩu xác nhận
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu xác nhận không khớp!";
                return View();
            }

            // Kiểm tra nếu người dùng đã tồn tại (kiểm tra trong danh sách người dùng)
            var existingUser = users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Tên người dùng đã tồn tại!";
                return View();
            }

            // Lưu thông tin người dùng vào danh sách
            users.Add(new User { Username = username, Password = password });

            // Lưu thông tin vào TempData để thông báo cho người dùng
            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login");
        }

        // Xử lý đăng xuất
        public ActionResult Logout()
        {
            // Xóa cookie xác thực
            FormsAuthentication.SignOut();

            // Xóa thông tin người dùng trong Session
            Session.Clear();

            // Chuyển hướng về trang đăng nhập sau khi đăng xuất
            return RedirectToAction("Login", "Account");
        }
    }
}
