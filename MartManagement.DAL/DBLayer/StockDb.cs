using MartManagement.BOL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MartManagement.DAL.DBLayer
{
    public class StockDb /*: _IAllRepository<BOL.Stock> where Stock : class*/
    {
        //connection string...... 
        private martmanagement_DbEntities _context;
        private IDbSet<Stock> dbEntity;
        public StockDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<Stock>();
        }

        public void DeleteModel(int modelID)
        {
            Stock model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<Stock> GetModel()
        {
            return dbEntity.ToList();
        }

        public Stock GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(Stock model)
        {
            dbEntity.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(BOL.Stock model)
        {
            Stock obj = _context.Stocks.Find(model.Stock_Id);
            obj.Stock_Quantity = model.Stock_Quantity;
            obj.Stock_RetailPrice = model.Stock_RetailPrice;
            obj.Stock_TotalPrice = model.Stock_RetailPrice * model.Stock_Quantity;
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
