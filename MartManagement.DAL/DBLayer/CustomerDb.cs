using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.DAL.DBLayer
{
    public class CustomerDb<Customer> : _IAllRepository<Customer> where Customer : class
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        private IDbSet<Customer> dbEntity;
        public CustomerDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<Customer>();
        }

        public void DeleteModel(int modelID)
        {
            Customer model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<Customer> GetModel()
        {
            return dbEntity.ToList();
        }

        public Customer GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(Customer model)
        {
            dbEntity.Add(model);
            Save();
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
