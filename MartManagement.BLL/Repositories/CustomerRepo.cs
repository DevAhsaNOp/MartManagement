using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;

namespace MartManagement.BLL.Repositories
{
    public class CustomerRepo
    {
        private CustomerDb dbObj;

        public CustomerRepo()
        {
            dbObj = new CustomerDb();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public Tuple<int, string> GetCustomerNameAndIdByPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return new Tuple<int, string>(0, "");

            return dbObj.GetCustomerNameAndIdByPhone(phone);
        }

        public IEnumerable<Customer> GetModel()
        {
            return dbObj.GetModel();
        }

        public Customer GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public int InsertModel(Customer model)
        {
            return dbObj.InsertModel(model);
        }

        public void UpdateModel(Customer model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
