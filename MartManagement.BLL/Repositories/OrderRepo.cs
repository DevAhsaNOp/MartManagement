using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BLL.Repositories
{
    public class OrderRepo
    {
        private OrderDb dbObj;

        public OrderRepo()
        {
            dbObj = new OrderDb();
        }

        public IEnumerable<Order> GetModel()
        {
            return dbObj.GetModel();
        }

        public Order GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public bool InsertModel(Order order, Customer customer)
        {
            return dbObj.AddOrder(order, customer);
        }

        public void UpdateModel(Order model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
