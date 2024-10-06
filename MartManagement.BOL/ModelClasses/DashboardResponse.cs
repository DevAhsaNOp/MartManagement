using System.Collections.Generic;

namespace MartManagement.BOL.ModelClasses
{
    public sealed class DashboardResponse
    {
        public long TotalCustomers { get; set; }
        public long TotalOrders { get; set; }
        public long TotalItems { get; set; }
        public long TotalCategories { get; set; }
        public List<OrdersPerDayResponse> OrdersPerDay { get; set; }
        public List<ItemWiseStockResponse> CurrentStocksDetails { get; set; }
        public ItemSalesByDayResponse DayWiseItemsSales { get; set; }
    }

    public sealed class OrdersPerDayResponse
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public sealed class ItemWiseStockResponse
    {
        public string ItemName { get; set; }
        public int Count { get; set; }
    }

    public sealed class ItemSalesByDayResponse
    {
        public List<ItemSalesResponse> ItemSales { get; set; }
        public List<string> UniqueDates { get; set; }
    }

    public sealed class ItemSalesResponse
    {
        public string ItemName { get; set; }
        public List<SalesByDayResponse> SalesByDay { get; set; }
    }

    public sealed class SalesByDayResponse
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }
}
