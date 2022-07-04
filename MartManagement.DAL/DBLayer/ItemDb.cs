using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MartManagement.DAL.DBLayer
{

    public class ItemDb<Item> : _IAllRepository<Item> where Item : class
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        private IDbSet<Item> dbEntity;
        public ItemDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<Item>();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            var result = new List<SelectListItem>();
            result = (from obj in _context.Items
                      select new SelectListItem()
                      {
                          Text = obj.Item_Name,
                          Value = obj.Item_Id.ToString(),
                          Selected = false
                      }).ToList();
            return result;
        }

        public void DeleteModel(int modelID)
        {
            Item model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<Item> GetModel()
        {
            return dbEntity.ToList();
        }

        public Item GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(Item model)
        {
            dbEntity.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(Item model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
