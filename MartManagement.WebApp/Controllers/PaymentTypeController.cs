using MartManagement.BLL.Repositories;
using MartManagement.BOL;
using System;
using System.Web.Mvc;

namespace MartManagement.WebApp.Controllers
{
    [Authorize]
    public class PaymentTypeController : Controller
    {
        private PaymentTypeRepo RepoObj;
        public PaymentTypeController()
        {
            RepoObj = new PaymentTypeRepo();
        }

        public ActionResult List()
        {
            return View(RepoObj.GetModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(PaymentType pay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RepoObj.InsertModel(pay);
                    TempData["SuccessMsg"] = "Payment Type Added Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on creating Payment Type!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.Payment = RepoObj.GetModelByID(id).PaymentType_Name;
            var data = RepoObj.GetModelByID(id);
            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(PaymentType pay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RepoObj.UpdateModel(pay);
                    TempData["SuccessMsg"] = "Payment Type Updated Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on updating Payment Type!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            ViewBag.Payment = RepoObj.GetModelByID(id).PaymentType_Name;
            return View(RepoObj.GetModelByID(id));
        }
    }
}