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
    public class StockController : Controller
    {
        private StockRepo RepoObj;
        private ItemRepo RepoObj1;
        private martmanagement_DbEntities _context;
        public StockController()
        {
            RepoObj = new StockRepo();
            RepoObj1 = new ItemRepo();
            _context = new martmanagement_DbEntities();
        }

        public ActionResult List()
        {
            return View(RepoObj.GetModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            ViewBag.Item_Id = new SelectList(_context.Items, "Item_Id", "Item_Name");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Stock stock)
        {
            var quant = RepoObj.GetModelByID(stock.Item_Id.GetValueOrDefault()).Item.Item_Stock;
            var ItemData = RepoObj1.GetModelByID(stock.Item_Id.GetValueOrDefault());
            var Price = RepoObj.GetModelByID(stock.Item_Id.GetValueOrDefault()).Item.Item_BuyPrice;
            try
            {
                if (stock.Stock_Quantity < quant && stock.Stock_RetailPrice > Price)
                {
                    if (ModelState.IsValid)
                    {
                        RepoObj.InsertModel(stock);
                        ItemData.Item_Stock -= stock.Stock_Quantity;                         
                        RepoObj1.UpdateModel(ItemData);
                        TempData["SuccessMsg"] = "Stock Added Successfully!";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return View();
                    }
                }
                else if (stock.Stock_RetailPrice < Price)
                {
                    TempData["ErrorMsg"] = "Item Retail price should be greater than buy price";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Stock Not Avalaible" ;
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on creating Stock!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.ItemName = RepoObj.GetModelByID(id).Item.Item_Name;
            var data = RepoObj.GetModelByID(id);
            ViewBag.Item_Id = new SelectList(_context.Items, "Item_Id", "Item_Name", data.Item_Id);
            return View(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Stock stock)
        {
            var ItemData = RepoObj1.GetModelByID(stock.Item_Id.GetValueOrDefault());
            var quant = RepoObj.GetModelByID(stock.Item_Id.GetValueOrDefault()).Item.Item_Stock;
            var Price = RepoObj.GetModelByID(stock.Item_Id.GetValueOrDefault()).Item.Item_BuyPrice;
            try
            {
                if (stock.Stock_Quantity < quant && stock.Stock_RetailPrice > Price)
                {
                    if (ModelState.IsValid)
                    {
                        RepoObj.UpdateModel(stock);
                        ItemData.Item_Stock -= stock.Stock_Quantity;
                        RepoObj1.UpdateModel(ItemData);
                        TempData["SuccessMsg"] = "Stock Updated Successfully!";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return View();
                    }
                }
                else if (stock.Stock_RetailPrice < Price)
                {
                    TempData["ErrorMsg"] = "Item Retail price should be greater than buy price";
                    return RedirectToAction("Edit");
                }
                else
                {
                    TempData["ErrorMsg"] = "Stock Not Avalaible";
                    return RedirectToAction("Edit");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Error occured on updating Stock!" + ex.Message;
                return RedirectToAction("List");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            ViewBag.ItemName = RepoObj.GetModelByID(id).Item.Item_Name;
            return View(RepoObj.GetModelByID(id));
        }
    }
}