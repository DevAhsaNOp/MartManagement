using MartManagement.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MartManagement.DAL.DBLayer
{
    public class SubCategoryDb<SubCategory> : _IAllRepository<SubCategory> where SubCategory : class
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        private IDbSet<SubCategory> dbEntity;
        public SubCategoryDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<SubCategory>();
        }

        public void DeleteModel(int modelID)
        {
            SubCategory model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<SubCategory> GetModel()
        {
            return dbEntity.ToList();
        }

        public SubCategory GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(SubCategory model)
        {
            dbEntity.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(SubCategory model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
