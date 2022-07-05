using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.DAL.DBLayer
{
    public class OrderDb /*<Order> : _IAllRepository<Order> where Order : class*/
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

        //public void InsertModel(OrderDetail model)
        //{
        //    _context.Orders.Add(model);
        //    Save();
        //}

        private CustomerDb CustomerRepositories = new CustomerDb();
        public bool AddOrder(Order orderViewModel, Customer customerViewModel, string[] quant)
        {
            try
            {
                var customerId = CustomerRepositories.InsertModel(customerViewModel);
                Order objOrder = new Order()
                {
                    Customer_Id = customerId,
                    Order_FinalTotal = orderViewModel.Order_FinalTotal,
                    Order_Date = DateTime.Now.Date,
                    Order_Number = String.Format("{0:ddmmyyyyhhmmss}", DateTime.Now),
                    PaymentType_Id = orderViewModel.PaymentType_Id,
                };
                _context.Orders.Add(objOrder);
                _context.SaveChanges();
                var OrderId = objOrder.Order_Id;
                int i = 0;
                foreach (var item in orderViewModel.OrderDetails)
                {
                    var objOrderDetails = new OrderDetail()
                    {
                        OrderDetail_Discount = item.OrderDetail_Discount,
                        Item_Id = item.Item_Id,
                        OrderDetail_Quantity = Convert.ToInt32(Convert.ToDecimal(quant[i])),
                        Order_Id = OrderId,
                        OrderDetail_FinalTotal = item.OrderDetail_FinalTotal,
                        OrderDetail_UnitPrice = item.OrderDetail_UnitPrice
                    };
                    _context.OrderDetails.Add(objOrderDetails);
                    _context.SaveChanges();

                    StockDb stockObj = new StockDb();
                    var stockData = _context.Stocks.Where(x => x.Item_Id == objOrderDetails.Item_Id).FirstOrDefault();
                    stockData.Stock_Quantity = stockData.Stock_Quantity - objOrderDetails.OrderDetail_Quantity;
                    stockObj.UpdateModel(stockData);
                    Transaction objTransaction = new Transaction()
                    {
                        Item_Id = item.Item_Id,
                        PaymentType_Id = orderViewModel.PaymentType_Id,
                        Transaction_Date = DateTime.Now,
                        Transaction_FinalTotal = objOrder.Order_FinalTotal,
                        OrderDetail_Id = objOrderDetails.OrderDetail_Id
                    };
                    _context.Transactions.Add(objTransaction);
                    _context.SaveChanges();
                    i++;
                }
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
