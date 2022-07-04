using MartManagement.BOL;
using MartManagement.DAL.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartManagement.BLL.Repositories
{
    public class PaymentTypeRepo
    {
        private PaymentTypeDb<PaymentType> dbObj;

        public PaymentTypeRepo()
        {
            dbObj = new PaymentTypeDb<PaymentType>();
        }
        public void DeleteModel(int modelID)
        {
            dbObj.DeleteModel(modelID);
        }

        public IEnumerable<PaymentType> GetModel()
        {
            return dbObj.GetModel();
        }

        public PaymentType GetModelByID(int modelId)
        {
            return dbObj.GetModelByID(modelId);
        }

        public void InsertModel(PaymentType model)
        {
            dbObj.InsertModel(model);
        }

        public void UpdateModel(PaymentType model)
        {
            dbObj.UpdateModel(model);
        }
    }
}
