using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System.Collections.Generic;

namespace MartManagement.BLL.Repositories
{
    public class SubCategoryRepo
    {
        private SubCategoryDb<SubCategory> dbObj;

        public SubCategoryRepo()
        {
            dbObj = new SubCategoryDb<SubCategory>();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public IEnumerable<SubCategory> GetModel()
        {
            return dbObj.GetModel();
        }

        public SubCategory GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public void InsertModel(SubCategory model)
        {
            dbObj.InsertModel(model);
        }

        public void UpdateModel(SubCategory model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
