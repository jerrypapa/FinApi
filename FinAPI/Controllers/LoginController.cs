using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinAPI.Controllers
{
    public class LoginController : Controller
    {
        FinAdmin finAdmin = new FinAdmin();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult L(LoginModel loginModel)
        {
            Users user = new Models.Users();
            var email = loginModel.Email;
            var password = loginModel.Password;

            finAdmin = user.Login(email, Crypto.Hash(password, "MD5"));

            if (finAdmin != null)
            {
                Session["logged_user"] = finAdmin;
                return RedirectToAction("Index", "ViewCheque", new { area = "" });
            }
            else
            {
                Session["logged_user"] = null;
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Logout()
        {
            Session.Remove("logged_user");

            return RedirectToAction("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
