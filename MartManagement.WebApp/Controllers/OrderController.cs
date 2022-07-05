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
    public class OrderController : Controller
    {
        private OrderRepo RepoObj;
        private martmanagement_DbEntities _context;
        public OrderController()
        {
            RepoObj = new OrderRepo();
            _context = new martmanagement_DbEntities();
        }

        public ActionResult List()
        {
            return View(RepoObj.GetModel());
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = _context.Stocks.Single(model => model.Item_Id == itemId).Stock_RetailPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            ItemRepo objitemRepositories = new ItemRepo();
            PaymentTypeRepo objpaymentTypeRepositories = new PaymentTypeRepo();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objitemRepositories.GetAllItems(), objpaymentTypeRepositories.GetAllPaymentType());
            return View(objMultipleModels);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Create(Order objOrderViewModel, Customer objCustomerViewModel)
        {
            objOrderViewModel.Customer_Id = 3;
            objCustomerViewModel.Customer_Id = 3;
            objCustomerViewModel.Customer_Name = "Naeem";
            objCustomerViewModel.Customer_Name = "0311280375";
            bool isStatus = RepoObj.InsertModel(objOrderViewModel, objCustomerViewModel);
            string SuccessMessage = String.Empty;

            if (isStatus)
            {
                SuccessMessage = "Your Order Has Been Successfully Placed.";
            }
            else
            {
                SuccessMessage = "There Is Some Issue While Placing Order.";
            }
            return Json(SuccessMessage, JsonRequestBehavior.AllowGet);
        }
    }
}