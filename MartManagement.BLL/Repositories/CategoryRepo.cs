using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BLL.Repositories
{
    public class CategoryRepo
    {
        private CatgoryDb<Category> dbObj;

        public CategoryRepo()
        {
            dbObj = new CatgoryDb<Category>();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public IEnumerable<Category> GetModel()
        {
            return dbObj.GetModel();
        }

        public Category GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public void InsertModel(Category model)
        {
            dbObj.InsertModel(model);
        }

        public void UpdateModel(Category model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
