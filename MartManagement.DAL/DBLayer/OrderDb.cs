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
        public bool AddOrder(Order orderViewModel, Customer customerViewModel)
        {
            try
            {
                var customerId = CustomerRepositories.InsertModel(customerViewModel);
                Order objOrder = new Order()
                {
                    Customer_Id = customerId,
                    Order_FinalTotal = orderViewModel.Order_FinalTotal,
                    Order_Date = orderViewModel.Order_Date,
                    Order_Number = String.Format("{0:ddmmyyyyhhmmss}", DateTime.Now),
                    PaymentType_Id = orderViewModel.PaymentType_Id,
                };
                _context.Orders.Add(objOrder);
                _context.SaveChanges();
                var OrderId = objOrder.Order_Id;

                foreach (var item in orderViewModel.OrderDetails)
                {
                    var objOrderDetails = new OrderDetail()
                    {
                        OrderDetail_Discount = item.OrderDetail_Discount,
                        Item_Id = item.Item_Id,
                        OrderDetail_Quantity = item.OrderDetail_Quantity,
                        Order_Id = item.Order_Id,
                        OrderDetail_FinalTotal = item.OrderDetail_FinalTotal,
                        OrderDetail_UnitPrice = item.OrderDetail_UnitPrice
                    };
                    _context.OrderDetails.Add(objOrderDetails);
                    _context.SaveChanges();

                    Transaction objTransaction = new Transaction()
                    {
                        Item_Id =item.Item_Id,
                        PaymentType_Id = orderViewModel.PaymentType_Id,
                        Transaction_Date = orderViewModel.Order_Date,
                        Transaction_FinalTotal = orderViewModel.Order_FinalTotal,
                        Order_Id = OrderId
                    };
                    _context.Transactions.Add(objTransaction);
                    _context.SaveChanges();
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
