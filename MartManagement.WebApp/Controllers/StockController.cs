using MartManagement.BLL.Repositories;
using MartManagement.BOL;
using System;
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
            var ItemData = RepoObj1.GetModelByID(stock.Item_Id.GetValueOrDefault());
            var StockData = RepoObj.GetModelByID(stock.Item_Id.GetValueOrDefault());
            try
            {
                if (stock.Stock_Quantity < ItemData.Item_Stock && stock.Stock_RetailPrice > ItemData.Item_BuyPrice)
                {
                    if (ModelState.IsValid)
                    {
                        if (StockData != null)
                        {
                            ItemData.Item_Stock -= stock.Stock_Quantity;
                            RepoObj1.UpdateModel(ItemData);
                            StockData.Stock_Quantity += stock.Stock_Quantity;
                            StockData.Stock_RetailPrice = stock.Stock_RetailPrice;
                            StockData.Stock_TotalPrice = stock.Stock_RetailPrice * StockData.Stock_Quantity;
                            RepoObj.UpdateModel(StockData);
                        }
                        else
                        {
                            RepoObj.InsertModel(stock);
                            ItemData.Item_Stock -= stock.Stock_Quantity;
                            RepoObj1.UpdateModel(ItemData);
                        }
                        TempData["SuccessMsg"] = "Stock Added Successfully!";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return View();
                    }
                }
                else if (stock.Stock_RetailPrice < ItemData.Item_BuyPrice)
                {
                    TempData["ErrorMsg"] = "Item Retail price should be greater than buy price";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Stock Not Avalaible";
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
            var StockData = RepoObj.GetModelByID(stock.Stock_Id).Stock_Quantity;
            var ItemData = RepoObj1.GetModelByID(stock.Item_Id.GetValueOrDefault());
            var StockQuantityData = stock.Stock_Quantity - StockData;
            try
            {
                if ((stock.Stock_Quantity < ItemData.Item_Stock && stock.Stock_RetailPrice > ItemData.Item_BuyPrice) ||
                    (stock.Stock_Quantity <= StockData && stock.Stock_RetailPrice > ItemData.Item_BuyPrice) ||
                    (StockQuantityData <= ItemData.Item_Stock && stock.Stock_RetailPrice > ItemData.Item_BuyPrice))
                {
                    if (ModelState.IsValid)
                    {
                        RepoObj.UpdateModel(stock);
                        if (stock.Stock_Quantity < StockData)
                        {
                            ItemData.Item_Stock = ItemData.Item_Stock + (StockData - stock.Stock_Quantity);
                            RepoObj1.UpdateModel(ItemData);
                        }
                        else if (StockQuantityData <= ItemData.Item_Stock && StockQuantityData > 0)
                        {
                            ItemData.Item_Stock -= StockQuantityData;
                            RepoObj1.UpdateModel(ItemData);
                        }
                        TempData["SuccessMsg"] = "Stock Updated Successfully!";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        return View();
                    }
                }
                else if (stock.Stock_RetailPrice < ItemData.Item_BuyPrice)
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