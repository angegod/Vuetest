using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Vuetest.signalr.hubs;

namespace Vuetest.Models
{
    public class Repository
    {
        SqlConnection co = new SqlConnection("Server=TR\\SQLEXPRESS;Database=Imgtext;uid=ange;pwd=ange0909;Trusted_Connection=True;MultipleActiveResultSets=True;");
        public List<Product> GetAllMessages()
        {
            SqlDependency.Start(co.ConnectionString);
            var messages = new List<Product>();

            
            using (var cmd = new SqlCommand(@"SELECT [itemId],[itemName],[itemCount],[itemPrice],[itemLocate] FROM [dbo].[storage]", co))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);

                int count,price;
                string id,name,locate;
                

                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    id = ds.Tables[0].Rows[i][0].ToString();
                    name = ds.Tables[0].Rows[i][1].ToString();
                    count= int.Parse(ds.Tables[0].Rows[i][2].ToString());
                    price= int.Parse(ds.Tables[0].Rows[i][3].ToString());
                    locate = ds.Tables[0].Rows[i][4].ToString();
                    messages.Add(new Product(id, name, count, price, locate));

                }
            }
            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table. 
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MyHub.SendMessages();
            }
        }
    }
}