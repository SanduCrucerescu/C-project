using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
    class MSSQL
    {
        private SqlConnection con = new SqlConnection(@"Data Source=localhost;Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Personal\WpfApp1\WpfApp1\auto.mdf;Integrated Security=True;Integrated security=true");

        public SqlConnection getConnection
        {
            get
            {
                return con;
            }
        }


        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        
    }
    class auto
    {
        MSSQL db = new MSSQL();

        public bool inserare(string mr, string md, string cl, string tara, int anul, string conb, double cap, string trans, int pret, string starea)
        {
            SqlCommand cmd = new SqlCommand("insert into automobile values (@mr,@md,@cl,@tara,@anul,@conb,@cap,@trans,@pret,@starea,CAST (GETDATE() AS DATE) )", db.getConnection);

            cmd.Parameters.Add("@mr", SqlDbType.VarChar).Value = mr;
            cmd.Parameters.Add("@md", SqlDbType.VarChar).Value = md;
            cmd.Parameters.Add("@cl", SqlDbType.VarChar).Value = cl;
            cmd.Parameters.Add("@tara", SqlDbType.VarChar).Value = tara;
            cmd.Parameters.Add("@anul", SqlDbType.Int).Value = anul;
            cmd.Parameters.Add("@conb", SqlDbType.VarChar).Value = conb;
            cmd.Parameters.Add("@cap", SqlDbType.Int).Value = cap;
            cmd.Parameters.Add("@trans", SqlDbType.VarChar).Value = trans;
            cmd.Parameters.Add("@pret", SqlDbType.Int).Value = pret;
            cmd.Parameters.Add("@starea", SqlDbType.VarChar).Value = starea;


            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            {
                db.closeConnection();
                return false;
            }
        }

        internal bool inserare(string mr, string md, string cl, string tara, int anul, string conb, double cap, string trans, int pret)
        {
            throw new NotImplementedException();
        }
        public DataTable getAuto(SqlCommand cmd)
        {
            cmd.Connection = db.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
        public bool delAuto(int autoid)
        {
            SqlCommand cmd = new SqlCommand("delete from automobile where id=@id", db.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = autoid;

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            {
                db.closeConnection();
                return false;
            }
        }
        public bool updateAuto(int id, string mr, string md, string cl, string tara, int anul, string conb, double cap, string trans, int pret, string starea)
        {
            SqlCommand cmd = new SqlCommand("update automobile set marca=@mr, model=@md, Culoare=@cl, tara=@tara, anul=@anul, conb=@conb, cap=@cap, trans=@trans, pret=@pret, Starea=@starea where id=@id", db.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@mr", SqlDbType.VarChar).Value = mr;
            cmd.Parameters.Add("@md", SqlDbType.VarChar).Value = md;
            cmd.Parameters.Add("@cl", SqlDbType.VarChar).Value = cl;
            cmd.Parameters.Add("@tara", SqlDbType.VarChar).Value = tara;
            cmd.Parameters.Add("@anul", SqlDbType.Int).Value = anul;
            cmd.Parameters.Add("@conb", SqlDbType.VarChar).Value = conb;
            cmd.Parameters.Add("@cap", SqlDbType.Int).Value = cap;
            cmd.Parameters.Add("@trans", SqlDbType.VarChar).Value = trans;
            cmd.Parameters.Add("@pret", SqlDbType.Int).Value = pret;
            cmd.Parameters.Add("@starea", SqlDbType.VarChar).Value = starea;
            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            {
                db.closeConnection();
                return false;
            }
        }
        public bool tara(string tara)
        {
            SqlCommand cmd = new SqlCommand("Select avg(pret) from automobile where tara=@tara", db.getConnection);
            cmd.Parameters.Add("@tara", SqlDbType.VarChar).Value = tara;

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            {
                db.closeConnection();
                return false;
            }
        }
    }
    class cump
    {
        MSSQL db = new MSSQL();

        public bool inserare(int idauto, string nume, string pren, string adres, string indp, DateTime data, int tel)
        {
            SqlCommand cmd = new SqlCommand("insert into cumparatori values (@idauto,@nm,@pr,@adres,@indp,@data,@tel)", db.getConnection);

            cmd.Parameters.Add("@idauto", SqlDbType.Int).Value = idauto;
            cmd.Parameters.Add("@nm", SqlDbType.VarChar).Value = nume;
            cmd.Parameters.Add("@pr", SqlDbType.VarChar).Value = pren;
            cmd.Parameters.Add("@adres", SqlDbType.VarChar).Value = adres;
            cmd.Parameters.Add("@indp", SqlDbType.Int).Value = indp;
            cmd.Parameters.Add("@data", SqlDbType.DateTime).Value = data;
            cmd.Parameters.Add("@tel", SqlDbType.Int).Value = tel;

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            {
                db.closeConnection();
                return false;
            }
        }

    }
}
