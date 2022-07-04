using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return  _context.Customers.ToList();
        }

        public Customer GetModelByID(int modelId)
        {
            return  _context.Customers.Find(modelId);
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
