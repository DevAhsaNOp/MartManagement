using MartManagement.BLL.Repositories;
using MartManagement.BOL;
using MartManagement.BOL.ValidationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MartManagement.WebApp.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private ItemRepo RepoObj;
        private martmanagement_DbEntities _context;
        public ItemController()
        {
            RepoObj = new ItemRepo();
            _context = new martmanagement_DbEntities();
        }
        public ActionResult List()
        {
            return View(RepoObj.GetModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            ViewBag.SubCategory_Id = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.Item_TotalPrice = item.Item_Stock * item.Item_BuyPrice;
                    RepoObj.InsertModel(item);
                    TempData["SuccessMsg"] = "Item Added Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on creating Item!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.ItemName = RepoObj.GetModelByID(id).Item_Name;
            var data = RepoObj.GetModelByID(id);
            ViewBag.SubCategory_Id = new SelectList(_context.SubCategories, "SubCategory_Id", "SubCategory_Name", data.SubCategory_Id);
            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.Item_TotalPrice = item.Item_Stock * item.Item_BuyPrice;
                    RepoObj.UpdateModel(item);
                    TempData["SuccessMsg"] = "Item Updated Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on updating Item!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            ViewBag.ItemName = RepoObj.GetModelByID(id).Item_Name;
            return View(RepoObj.GetModelByID(id));
        }

        public JsonResult GetNotificationItem()
        {
            var ItemsLimit = 10;
            NotificationComponent NC = new NotificationComponent();
            var list = NC.GetItems(ItemsLimit);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            Session["ListItem"] = list;
            string abc = Session["ListItem"].ToString();
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}