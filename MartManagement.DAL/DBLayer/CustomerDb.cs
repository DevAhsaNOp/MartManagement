using MartManagement.BOL;
using MartManagement.BOL.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartManagement.DAL.DBLayer
{
    public class CustomerDb
    {
        private martmanagement_DbEntities _context;
        public CustomerDb()
        {
            _context = new martmanagement_DbEntities();
        }

        public void DeleteModel(int modelID)
        {
            Customer model = _context.Customers.Find(modelID);
            _context.Customers.Remove(model);
            Save();
        }

        public IEnumerable<Customer> GetModel()
        {
            return _context.Customers.ToList();
        }

        public Tuple<int, string> GetCustomerNameAndIdByPhone(string phone)
        {
            var data = _context.Customers.Where(x => x.Customer_PhoneNumber == phone)
                .Select(x => new { x.Customer_Name, x.Customer_Id })
                .FirstOrDefault();

            if (data != null)
                return new Tuple<int, string>(data.Customer_Id, data.Customer_Name);

            return new Tuple<int, string>(0, "");
        }

        public Customer GetModelByID(int modelId)
        {
            return _context.Customers.Find(modelId);
        }

        public int InsertModel(Customer model)
        {
            _context.Customers.Add(model);
            Save();
            return model.Customer_Id;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(Customer model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public DashboardResponse GetDashboardDetails()
        {
            var customersCount = _context.Customers.Count();
            var ordersCount = _context.Orders.Count();
            var itemsCount = _context.Items.Count();
            var categoriesCount = _context.Categories.Count();
            var allOrders = _context.Orders.ToList();
            var ordersPerDay = allOrders.GroupBy(x => x.Order_Date.Date)
                .Select(x => new { Date = x.Key, Count = x.Count() })
                .OrderBy(x => x.Date)
                .Select(x => new OrdersPerDayResponse
                {
                    Date = x.Date.ToString("dd-MM-yyyy"),
                    Count = x.Count
                })
                .ToList();

            var currentStocksDetails = _context.Stocks.GroupBy(x => x.Item.Item_Name)
                .Select(x => new { ItemName = x.Key, Count = x.Count() })
                .OrderBy(x => x.ItemName)
                .Select(x => new ItemWiseStockResponse
                {
                    ItemName = x.ItemName,
                    Count = x.Count
                })
                .ToList();

            var dayWiseItemsSales = allOrders.SelectMany(x => x.OrderDetails)
                .GroupBy(x => new { x.Order.Order_Date.Date, x.Item.Item_Name })
                .Select(x => new { x.Key.Date.Date, ItemName = x.Key.Item_Name, Count = x.Sum(y => y.OrderDetail_Quantity) })
                .OrderBy(x => x.Date.Date)
                .ThenBy(x => x.ItemName)
                .ToList();

            var groupedData = dayWiseItemsSales
                .GroupBy(x => x.ItemName)
                .Select(g => new ItemSalesResponse
                {
                    ItemName = g.Key,
                    SalesByDay = g
                    .Select(s => new SalesByDayResponse
                    {
                        Date = s.Date.Date.ToString("dd-MM-yyyy"),
                        Count = s.Count
                    }).ToList()
                })
                .ToList();

            var uniqueDates = dayWiseItemsSales
                .Select(x => x.Date.Date.ToString("dd-MM-yyyy"))
                .Distinct()
                .OrderBy(date => date)
                .ToList();


            return new DashboardResponse
            {
                TotalCategories = categoriesCount,
                TotalCustomers = customersCount,
                TotalItems = itemsCount,
                TotalOrders = ordersCount,
                OrdersPerDay = ordersPerDay,
                CurrentStocksDetails = currentStocksDetails,
                DayWiseItemsSales = new ItemSalesByDayResponse
                {
                    ItemSales = groupedData,
                    UniqueDates = uniqueDates
                }
            };
        }
    }
}
