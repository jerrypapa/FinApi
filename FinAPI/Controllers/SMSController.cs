using AfricasTalkingCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinAPI.Controllers
{
    public class SMSController : Controller
    {
        private static string apikey = "e952920d25a20cc9a8144ae200363d722f3459273815201914d8d4603e59d047";
        private static string username = "sandbox";
        private static AfricasTalkingGateway _atGWInstance = new AfricasTalkingGateway(username, apikey);

        [HttpPost]
        public ActionResult SendSms()
        {
            var phoneNumber = "+254715812661";
            var message = "Hello Derick";
            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message);
            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
           // Assert.IsTrue(success, "Should successfully send message to a valid phone number");
            return Content(""+success);
        }
        // GET: SMS
        public ActionResult Index()
        {
            var phoneNumber = "+254715812661";
            var message = "Hello Derick";
            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message);
            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            return View();
        }

        // GET: SMS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SMS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMS/Create
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

        // GET: SMS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SMS/Edit/5
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

        // GET: SMS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SMS/Delete/5
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
