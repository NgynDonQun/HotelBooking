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

        public ActionResult Edit() { 
            return View();
        }
    }
}