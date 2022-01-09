using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace WpfApp1
{
    public class DataItem
    {
        public string model { get; set; }
        public int nor { get; set; }
    }

    public class Data : ObservableCollection<DataItem>
    {
        public Data()
        {
            //MSSQL sql = new MSSQL();
            //var data = new ObservableCollection<DataItem>();

            //using (SqlConnection connection = sql.getConnection)
            //{
            //    connection.Open();
            //    string query = "select marca,count(*) as nor from  automobile group by marca,int";
            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                data.Add(new DataItem { model = reader.GetString(0), nor = Int32.Parse(reader.GetString(1)) });
            //            }
            //        }
            //    }
            //}
        }
         


    }
}
