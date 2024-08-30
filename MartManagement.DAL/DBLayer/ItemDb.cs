using MartManagement.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var result = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "Select Item",
                    Value = "0",
                    Selected = true,
                }
            };

            var allAvailableItems = _context.Items
                .Select(model => new SelectListItem
                {
                    Text = model.Item_Name,
                    Value = model.Item_Id.ToString(),
                    Selected = false
                })
                .ToList();

            if (allAvailableItems != null && allAvailableItems.Any())
                result.AddRange(allAvailableItems);

            return result;
        }

        public decimal GetItemUnitPrice(int itemId)
        {
            if (itemId == 0)
                return 0.0m;

            decimal UnitPrice = _context.Stocks.Single(model => model.Item_Id == itemId).Stock_RetailPrice;
            return UnitPrice;
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
            _context.Entry(model).State = EntityState.Modified;
            Save();
        }
    }
}
