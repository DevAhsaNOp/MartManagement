using System.Collections.Generic;

namespace MartManagement.BOL.ModelClasses
{
    public sealed class DashboardResponse
    {
        public long TotalCustomers { get; set; }
        public long TotalOrders { get; set; }
        public long TotalItems { get; set; }
        public long TotalCategories { get; set; }
        public List<int> OrdersPerDay { get; set; }
        public List<int> StocksPerDay { get; set; }
    }
}
