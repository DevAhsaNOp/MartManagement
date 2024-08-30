using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartManagement.DAL.DBLayer
{
    public class OrderDb
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        public OrderDb()
        {
            _context = new martmanagement_DbEntities();
        }

        public IEnumerable<Order> GetModel()
        {
            return _context.Orders.ToList();
        }

        public Order GetModelByID(int modelId)
        {
            return _context.Orders.Find(modelId);
        }

        private CustomerDb CustomerRepositories = new CustomerDb();
        public bool AddOrder(Order orderViewModel, Customer customerViewModel)
        {
            try
            {
                int customerId = 0;

                if (customerViewModel.Customer_Id <= 0)
                    customerId = CustomerRepositories.InsertModel(customerViewModel);
                else
                    customerId = customerViewModel.Customer_Id;

                Order objOrder = new Order()
                {
                    Customer_Id = customerId,
                    Order_Number = String.Format("{0:ddMMyyyyhhmmss}", DateTime.Now),
                    Order_Date = DateTime.Now,
                    Order_FinalTotal = orderViewModel.Order_FinalTotal,
                    PaymentType_Id = orderViewModel.PaymentType_Id
                };

                _context.Orders.Add(objOrder);
                _context.SaveChanges();

                var OrderId = objOrder.Order_Id;

                foreach (var item in orderViewModel.OrderDetails)
                {
                    var objOrderDetails = new OrderDetail()
                    {
                        Order_Id = OrderId,
                        Item_Id = item.Item_Id,
                        OrderDetail_UnitPrice = item.OrderDetail_UnitPrice,
                        OrderDetail_Quantity = item.OrderDetail_Quantity,
                        OrderDetail_Discount = item.OrderDetail_Discount,
                        OrderDetail_FinalTotal = item.OrderDetail_FinalTotal,
                    };
                    _context.OrderDetails.Add(objOrderDetails);
                    _context.SaveChanges();

                    StockDb stockObj = new StockDb();
                    var stockData = _context.Stocks.Where(x => x.Item_Id == objOrderDetails.Item_Id).FirstOrDefault();
                    stockData.Stock_Quantity = stockData.Stock_Quantity - objOrderDetails.OrderDetail_Quantity;
                    stockObj.UpdateModel(stockData);
                }

                Transaction objTransaction = new Transaction()
                {
                    PaymentType_Id = orderViewModel.PaymentType_Id,
                    Transaction_Date = DateTime.Now,
                    Transaction_FinalTotal = objOrder.Order_FinalTotal,
                    Order_Id = OrderId
                };

                _context.Transactions.Add(objTransaction);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(Order model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
