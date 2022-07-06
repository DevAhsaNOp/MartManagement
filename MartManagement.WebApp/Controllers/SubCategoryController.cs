using MartManagement.BLL.Repositories;
using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MartManagement.WebApp.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        private SubCategoryRepo RepoObj;
        private martmanagement_DbEntities _context;
        public SubCategoryController()
        {
            RepoObj = new SubCategoryRepo();
            _context = new martmanagement_DbEntities();
        }

        public ActionResult List()
        {
            return View(RepoObj.GetModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(_context.Categories, "Category_Id", "Category_Name");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SubCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RepoObj.InsertModel(category);
                    TempData["SuccessMsg"] = "Sub Category Added Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on creating Sub category!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryName = RepoObj.GetModelByID(id).SubCategory_Name;
            var data = RepoObj.GetModelByID(id);
            ViewBag.Category_Id = new SelectList(_context.Categories, "Category_Id", "Category_Name", data.Category_Id);
            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(SubCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RepoObj.UpdateModel(category);
                    TempData["SuccessMsg"] = "Sub Category Updated Successfully!";
                    return RedirectToAction("List");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on updating Sub category!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            ViewBag.CategoryName = RepoObj.GetModelByID(id).SubCategory_Name;
            return View(RepoObj.GetModelByID(id));
        }

    }
}