using Microsoft.AspNetCore.Mvc;
using MongoBackend1.Models;
using System;
using System.Diagnostics;

namespace MongoBackend1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();
            ViewBag.UserList = db.getUsers();

            return View();
        }

        public IActionResult insertUser(string txtname, string txtaddress, int txtphone, string txtemail)
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();
            db.insertUser(new User()
            {
                name = txtname,
                email = txtemail,
                phone = txtphone,
                address = txtaddress,
                dateIn = DateTime.Now
            });
            return RedirectToAction("Index", "Home");
        }

        public IActionResult updateUser(string txtid, string txtname, string txtaddress, int txtphone, string txtemail)
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();

            db.updateUser(txtid, txtname, txtaddress, txtphone, txtemail);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult deleteUser(string txtid)
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();
            User user = new User()
            {
                _id = ObjectId.Parse(txtid)
            };
            db.deleteUser(user);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}