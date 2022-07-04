﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.DAL
{
    interface _IAllRepository<T> where T : class
    {
        IEnumerable<T> GetModel(); //all data in the form of list..............
        T GetModelByID(int modelId); //find by id
        void InsertModel(T model); //insert
        void DeleteModel(int modelID); //delete
        void UpdateModel(T model); //update 
        void Save(); //changes save..................
    }
}
