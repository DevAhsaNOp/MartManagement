﻿using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BLL.Repositories
{
    public class ItemRepo
    {
        private ItemDb<Item> dbObj;

        public ItemRepo()
        {
            dbObj = new ItemDb<Item>();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public IEnumerable<Item> GetModel()
        {
            return dbObj.GetModel();
        }

        public Item GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public void InsertModel(Item model)
        {
            dbObj.InsertModel(model);
        }

        public void UpdateModel(Item model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
