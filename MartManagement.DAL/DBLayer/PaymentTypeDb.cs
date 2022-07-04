﻿using MartManagement.BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.DAL.DBLayer
{

    public class PaymentTypeDb<PayementType> : _IAllRepository<PayementType> where PayementType : class
    {

        private martmanagement_DbEntities _context;
        private IDbSet<PayementType> dbEntity;
        public PaymentTypeDb()
        {
            _context = new martmanagement_DbEntities();
            dbEntity = _context.Set<PayementType>();
        }

        public void DeleteModel(int modelID)
        {
            PayementType model = dbEntity.Find(modelID);
            dbEntity.Remove(model);
            Save();
        }

        public IEnumerable<PayementType> GetModel()
        {
            return dbEntity.ToList();
        }

        public PayementType GetModelByID(int modelId)
        {
            return dbEntity.Find(modelId);
        }

        public void InsertModel(PayementType model)
        {
            dbEntity.Add(model);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateModel(PayementType model)
        {
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
