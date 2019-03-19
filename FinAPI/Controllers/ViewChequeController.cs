using FinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FinAPI.Controllers
{
    public class ViewChequeController : Controller
    {
        Cheque c = new Cheque();
        IssuedCheque isCheque = new IssuedCheque();
        ReturnReasons rr = new ReturnReasons();
        List<ReturnReasons> rrList = new List<ReturnReasons>();
        // GET: ViewCheque
        public ActionResult Index()
        {
            Cheque c = new Cheque();
            int j = c.GetChequesCount();
            Cheque cheque = new Cheque {
                FImage="Fimage",
                BImage="BImage",
                Amount=j
            };
            List<Cheque> chequesList = c.GetCheques();
            //chequesList.Add(cheque);
            ViewBag.Message = chequesList;
            Session["login"] = "a";
            //Session.Remove("uname");
            /*if (Session["uname"] == null)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                return Content("Welcome " + Session["uname"]);
            }*/
            return View();
        }

        public ActionResult Search() {
            String search_key= Request.Form["search"];
            List<Cheque> chequeList = c.AdminSearchCheque(search_key);

            var serializer = new JavaScriptSerializer();

            var json = serializer.Serialize(chequeList);

            return Content(json);
        }


        public ActionResult AcceptCheque(String id)
        {
            int acceptCheque = c.AcceptCheque(id);
            String s = isCheque.pushNotification(id);
            //return RedirectToAction("Index");
            return RedirectToAction("Edit", "ViewCheque", new { area = "", id = id });
            //return Content(id);
        }

        [HttpPost]
        public ActionResult RejectCheque(RejectedCheque rC)
        {
            var chequeName = rC.ChequeName;
            var reasons = rC.Reasons;
            //var amount
            int rejected = c.RejectCheque(rC);

            /*if (rejected == 1)
            {
                return RedirectToAction("Edit", "ViewCheque", new { area = "", id = rC.ChequeName });
            }*/

            //return RedirectToAction("Index");
            return RedirectToAction("Edit", "ViewCheque", new { area = "", id = rC.ChequeName });
            //return Content(rC.ChequeName);
        }

        // GET: ViewCheque/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ViewCheque/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewCheque/Create
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

        // GET: ViewCheque/Edit/5
        public ActionResult Edit(String id)
        {
            Cheque cheque = c.getChequeDetails(id);
            List<ReturnReasons> rList = rr.getReturnResaons();

            ViewBag.Message = cheque;
            ViewBag.Reasons = rList;
            return View();
        }

        // POST: ViewCheque/Edit/5
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

        // GET: ViewCheque/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewCheque/Delete/5
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
