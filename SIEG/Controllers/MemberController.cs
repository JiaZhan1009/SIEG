﻿using Microsoft.AspNetCore.Mvc;
using SIEG.Models;
using System.Diagnostics;

namespace SIEG.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Member_buyerorder()
        {
            return View();
        }

        public IActionResult Member_personal()
        {
            return View();
        }

        public IActionResult Member_Membermodification()
        {
            return View();
        }

        public IActionResult Member_coupon()
        {
            return View();
        }

        public IActionResult Member_ProductCollection()
        {
            return View();
        }
        public IActionResult SellerAddProduct()
        {
            return View();
        }

        public IActionResult FavoritePosts()
        {
            return View();
        }

        public IActionResult SettingCreditcard()
        {
            return View();
        }

        public IActionResult PaymentInformation()
        {
            return View();
        }

        public IActionResult Mailinginformation()
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
