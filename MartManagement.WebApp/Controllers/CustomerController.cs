using MartManagement.BLL.Repositories;
using MartManagement.BOL;
using System;
using System.Web.Mvc;

namespace MartManagement.WebApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private CustomerRepo RepoObj;
        public CustomerController()
        {
            RepoObj = new CustomerRepo();
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
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var val = RepoObj.InsertModel(customer);
                    if (val > 0)
                    {
                        TempData["SuccessMsg"] = "Customer Added Successfully!";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Error occured on creating customer!";
                        return RedirectToAction("Create");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on creating customer!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.CustomerName = RepoObj.GetModelByID(id).Customer_Name;
            var data = RepoObj.GetModelByID(id);
            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RepoObj.UpdateModel(customer);
                    TempData["SuccessMsg"] = "Customer Updated Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on updating customer!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            ViewBag.CustomerName = RepoObj.GetModelByID(id).Customer_Name;
            return View(RepoObj.GetModelByID(id));
        }
    }
}