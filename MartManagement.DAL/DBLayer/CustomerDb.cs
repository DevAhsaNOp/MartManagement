using MartManagement.BOL;
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
    }
}
