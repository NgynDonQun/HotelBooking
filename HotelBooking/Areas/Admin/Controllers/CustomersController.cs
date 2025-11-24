using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        public readonly DatabaseDataContext _db;

        public CustomersController()
        {
            _db = new DatabaseDataContext();
        }
        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/Customers/GetAllCustomers - AJAX
        public ActionResult GetAllCustomers()
        {
            try
            {
                var customers = _db.Customers
                    .Select(c => new 
                    {
                        UserId = c.UserId,
                        FullName = c.FullName ?? "Chua Dat Ten",
                        Phone = c.Phone ?? "Chua co so dien thoai",
                        LoyaltyTierId = c.LoyaltyTierId ?? 0,
                        TotalPoints = c.TotalPoints
                    })
                    .ToList();
                return Json(new { success = true, data = customers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/Customers/DeleteCustomer - AJAX
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var customer = _db.Customers.FirstOrDefault(c => c.UserId == id);
                if (customer == null)
                    return Json(new { success = false, message = "Khách hàng không tồn tại" });
                _db.Customers.DeleteOnSubmit(customer);
                _db.SubmitChanges();
                return Json(new { success = true, message = "Xóa khách hàng thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        public ActionResult GetCustomerById(int id)
        {
            ViewBag.UserId = id;
            try
            {
                var customer = _db.Customers.Where(c => c.UserId == id).Select(c => new
                {
                    Name = c.FullName,
                    Phone = c.Phone
                }).FirstOrDefault();

                if (customer == null)
                    return Json(new { success = false, message = "Khách hàng không tồn tại" }, JsonRequestBehavior.AllowGet);
                return Json(new { success = true, data = customer }, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = "Lỗi" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCustomer(int UserId, string FullName, string Phone)
        {
            try
            {
                var customer = _db.Customers.FirstOrDefault(c => c.UserId == UserId);
                if (customer == null)
                    return Json(new { success = false, message = "Khách hàng không tồn tại" });

                customer.FullName = FullName;
                customer.Phone = Phone;
                _db.SubmitChanges();

                return Json(new { success = true, message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
        //[HttpPost]
        //public ActionResult CreateCustomer(string FullName, string Phone, string Email = "", string Address = "", string Notes = "")
        //{
        //    try
        //    {
        //        // Kiểm tra trùng số điện thoại (tùy chọn)
        //        var exist = _db.Customers.Any(c => c.Phone == Phone);
        //        if (exist)
        //            return Json(new { success = false, message = "Số điện thoại đã tồn tại!" });
                
        //        var customer = new global::Customer
        //        {
        //            FullName = FullName,
        //            Phone = Phone,
        //            Email = Email,
        //            Address = Address,
        //            Notes = Notes,
        //            CreatedDate = DateTime.Now,
        //            TotalPoints = 0
        //        };

        //        _db.Customers.InsertOnSubmit(customer);
        //        _db.SubmitChanges();

        //        return Json(new { success = true, message = "Thêm khách hàng thành công!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Lỗi: " + ex.Message });
        //    }
        //}

    }
}