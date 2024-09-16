using MartManagement.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MartManagement.DAL.DBLayer
{
    public class CatgoryDb<Category> : _IAllRepository<Category> where Category : class
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        private IDbSet<Category> dbEntity;
        public CatgoryDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<Category>();
        }

        public void DeleteModel(int modelID)
        {
            Category model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<Category> GetModel()
        {
            return dbEntity.ToList();
        }

        public Category GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(Category model)
        {
            dbEntity.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(Category model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
