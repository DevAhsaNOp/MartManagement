using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BLL.Repositories
{
    public class StockRepo
    {
        private StockDb dbObj;

        public StockRepo()
        {
            dbObj = new StockDb();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public IEnumerable<Stock> GetModel()
        {
            return dbObj.GetModel();
        }

        public Stock GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public void InsertModel(Stock model)
        {
            dbObj.InsertModel(model);
        }

        public void UpdateModel(Stock model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
