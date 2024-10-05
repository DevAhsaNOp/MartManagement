using MartManagement.BOL;
using MartManagement.BOL.ModelClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace MartManagement.WebApp
{
    public class NotificationComponent
    {
        //Here we will add a function for register notification (will add sql dependency)
        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
            string sqlCommand = @"SELECT [Item_Id]
      ,[SubCategory_Id]
      ,[Item_Name]
      ,[Item_BuyPrice]
      ,[Item_Stock]
      ,[Item_TotalPrice]
  FROM [dbo].[Item] where [Item_Stock] < 10";
            //you can notice here I have added table name like this [dbo].[Items] with [dbo], its mendatory when you use Sql Dependency
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                //cmd.Parameters.AddWithValue("@AddedOn", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we must have to execute the command here
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now
                }
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                NotificationHub.Send();

                //re-register notification
                RegisterNotification(DateTime.Now);
            }
        }

        public List<ItemCls> GetItems(int stock)
        {
            using (martmanagement_DbEntities dc = new martmanagement_DbEntities())
            {
                return dc.Items.Where(a => a.Item_Stock < stock).OrderBy(b => b.Item_Stock).Select(s => new ItemCls() { Item_Name = s.Item_Name, Item_Stock = s.Item_Stock }).ToList();
            }
        }
    }
}