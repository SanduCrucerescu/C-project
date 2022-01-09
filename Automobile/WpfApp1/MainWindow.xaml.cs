using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.BinaryDrawingFormat;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tara_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Adauga_Click(object sender, RoutedEventArgs e)
        {
            bool verif()
            {
                if ((model.Text.Trim() == "") ||
                    (Culoarea.Text.Trim() == "") ||
                    (tarap.Text.Trim() == "") ||
                    (Capmot.Text.Trim() == "") ||
                    (Pret.Text.Trim() == "") ||
                    (Marca.Text.Trim() == "") ||
                    (Anulp.Text.Trim() == "") ||
                    (Transmisie.Text.Trim() == ""))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            auto autom = new auto();

            string mr = Marca.Text;
            string md = model.Text;
            string cl = Culoarea.Text;
            string tara = tarap.Text;
            int anul = Int32.Parse(Anulp.Text);
            string conb = "Benzina";
            if (Dizel.IsChecked == true)
            {
                conb = "Disel";
            }

            double cap = Convert.ToDouble(Capmot.Text);
            string trans = Transmisie.Text;
            int pret = Convert.ToInt32(Pret.Text);
            string starea = "In Lot";

            if (verif())
            {
                if (autom.inserare(mr, md, cl, tara, anul, conb, cap, trans, pret, starea))
                {
                    MessageBox.Show("Automobil Adaugat", "Automobil adauga", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    Marca.Text = "";
                    model.Text = "";
                    tarap.Text = "";
                    Anulp.Text = "";
                    Culoarea.Text = "";
                    Capmot.Text = "";
                    Transmisie.Text = "";
                    Pret.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("Eroare", "Automobil adauga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Camp ", "Automobil adauga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }



        private void Marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Transmisie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void load_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Lista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            tabControl1.SelectedItem = Edit;
            e.Handled = true;
            DataGrid Lista = (DataGrid)sender;
            DataRowView row_selected = Lista.SelectedItems as DataRowView;
            DataGridCellInfo cell0 = Lista.SelectedCells[0];
            ID.Text = ((TextBlock)cell0.Column.GetCellContent(cell0.Item)).Text;
            DataGridCellInfo cell1 = Lista.SelectedCells[1];
            Marca1.Text = ((TextBlock)cell1.Column.GetCellContent(cell1.Item)).Text;
            DataGridCellInfo cell2 = Lista.SelectedCells[2];
            model1.Text = ((TextBlock)cell2.Column.GetCellContent(cell2.Item)).Text;
            DataGridCellInfo cell3 = Lista.SelectedCells[3];
            Culoarea1.Text = ((TextBlock)cell3.Column.GetCellContent(cell3.Item)).Text;
            DataGridCellInfo cell4 = Lista.SelectedCells[5];
            Anulp1.Text = ((TextBlock)cell4.Column.GetCellContent(cell4.Item)).Text;
            DataGridCellInfo cell5 = Lista.SelectedCells[4];
            tarap1.Text = ((TextBlock)cell5.Column.GetCellContent(cell5.Item)).Text;
            DataGridCellInfo cell6 = Lista.SelectedCells[6];

            if (((TextBlock)cell6.Column.GetCellContent(cell6.Item)).Text == "Disel")
            {
                Dizel1.IsChecked = true;
            }
            else
            {
                Benzina1.IsChecked = true;
            }
            DataGridCellInfo cell7 = Lista.SelectedCells[7];
            Capmot1.Text = ((TextBlock)cell7.Column.GetCellContent(cell7.Item)).Text;
            DataGridCellInfo cell8 = Lista.SelectedCells[8];
            Transmisie1.Text = ((TextBlock)cell8.Column.GetCellContent(cell8.Item)).Text;
            DataGridCellInfo cell9 = Lista.SelectedCells[9];
            Pret1.Text = ((TextBlock)cell9.Column.GetCellContent(cell9.Item)).Text;
            DataGridCellInfo cell10 = Lista.SelectedCells[10];
            Starea.Text = ((TextBlock)cell10.Column.GetCellContent(cell10.Item)).Text;

        }

        private void TabItem_MouseEnter(object sender, MouseEventArgs e)
        {
            MSSQL sql = new MSSQL();
            auto autom = new auto();
            SqlCommand cmd = new SqlCommand("select * from automobile order by marca asc", sql.getConnection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("automobile");
            sda.Fill(dt);
            Lista.ItemsSource = dt.DefaultView;
            Lista.IsReadOnly = true;

            SqlCommand cmd4 = new SqlCommand("select marca,count(*) as nor from  automobile group by marca having count(*) = (select max(nor) from (select marca, count(*) as nor from automobile group by marca) automobile )", sql.getConnection);

            sql.openConnection();
            SqlDataReader myReader4 = null;
            myReader4 = cmd4.ExecuteReader();
            while (myReader4.Read())
            {
                num.Content = myReader4["nor"].ToString();
                mar.Content = myReader4["marca"].ToString();

            }

            myReader4.Close();


            



        }

        private void cauta_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Sterge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auto autom = new auto();
                int id = Convert.ToInt32(ID.Text);
                if (MessageBox.Show("Dorit sa stergeti automobilul dat", "Stergere", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (autom.delAuto(id))
                    {
                        MessageBox.Show("Automobil Sters", "Sergere Automobil", MessageBoxButton.OK, MessageBoxImage.Information);
                        ID.Text = "";
                        Marca1.Text = "";
                        model1.Text = "";
                        tarap1.Text = "";
                        Anulp1.Text = "";
                        Culoarea1.Text = "";
                        Capmot1.Text = "";
                        Transmisie1.Text = "";
                        Pret1.Text = "";
                        Starea.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Automobilul nu a fost sters", "Sergere Automobil", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch
            {
            }
        }

        private void cump_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Adaugacump_Click(object sender, RoutedEventArgs e)
        {
            Cumparator cum = new Cumparator();
            cum.Show();
        }

        private void Edit_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auto autom = new auto();
                int id = Convert.ToInt32(ID.Text);
                SqlCommand cmd = new SqlCommand("select * from automobile where id=" + id);


                DataTable table = autom.getAuto(cmd);

                if (table.Rows.Count > 0)
                {
                    Marca1.Text = table.Rows[0]["Marca"].ToString();
                    model1.Text = table.Rows[0]["Model"].ToString();
                    Culoarea1.Text = table.Rows[0]["Culoare"].ToString();
                    tarap1.Text = table.Rows[0]["Tara"].ToString();
                    Anulp1.Text = table.Rows[0]["Anul"].ToString();
                    if (table.Rows[0]["Conb"].ToString() == "Benzina")
                    {
                        Benzina1.IsChecked = true;
                    }
                    else
                    {
                        Dizel1.IsChecked = true;
                    }
                    Capmot1.Text = table.Rows[0]["Cap"].ToString();
                    Transmisie1.Text = table.Rows[0]["Trans"].ToString();
                    Pret1.Text = table.Rows[0]["Pret"].ToString();
                    Starea.Text = table.Rows[0]["Starea"].ToString();

                }
            }
            catch
            {

            }
        }

        private void Edit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Edit_Loaded(object sender, RoutedEventArgs e)
        {
            ID.Text = "";
            Marca1.Text = "";
            model1.Text = "";
            tarap1.Text = "";
            Anulp1.Text = "";
            Culoarea1.Text = "";
            Capmot1.Text = "";
            Transmisie1.Text = "";
            Pret1.Text = "";
            Starea.Text = "";
        }

        private void taralista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MSSQL db = new MSSQL();
            auto autom = new auto();
            SqlCommand cmd = new SqlCommand("select avg(pret) as mediu from automobile where tara=@tara", db.getConnection);
            cmd.Parameters.Add("@tara", SqlDbType.VarChar).Value = taralista.SelectedItem;
            db.openConnection();
            SqlDataReader myReader = null;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                pretmediu.Content = myReader["mediu"].ToString();

            }
        }

        private void perioada_Click(object sender, RoutedEventArgs e)
        {
            MSSQL sql = new MSSQL();
            auto autom = new auto();
            DataGridCell myCell = new DataGridCell();
            SqlCommand cmd = new SqlCommand("select * from automobile where anul BETWEEN @a and @b and marca in ('BMW','Audi') ORDER BY marca asc ", sql.getConnection);
            cmd.Parameters.Add("@a", SqlDbType.Int).Value = din.Text;
            cmd.Parameters.Add("@b", SqlDbType.Int).Value = Pana.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet dt = new DataSet("auto");
            DataTable at = new DataTable("auto");
            sda.Fill(at);
            an.ItemsSource = at.DefaultView;
            an.IsReadOnly = true;


            SqlCommand del = new SqlCommand("delete  from BMW truncate table bmw", sql.getConnection);
            sql.openConnection();
            del.ExecuteNonQuery();
            sql.closeConnection();





        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            

            //using(var conn = sql.getConnection)
            //{
            //    conn.Open();
            //    var bulkCopy = new SqlBulkCopy(conn)
            //    {

            //        DestinationTableName =
            //                        $"[dbo].[BMW]"
            //    };

            //    bulkCopy.WriteToServer(((DataView)an.ItemsSource).Table);

            //}

            MSSQL sql = new MSSQL();
            auto autom = new auto();
            SqlCommand del = new SqlCommand("delete  from bmw truncate table bmw", sql.getConnection);
            sql.openConnection();
            del.ExecuteNonQuery();
            sql.closeConnection();
            SqlCommand cm = new SqlCommand("insert into bmw select * from automobile where anul BETWEEN @a and @b and marca in ('BMW','Audi') ORDER BY marca asc ", sql.getConnection);
            cm.Parameters.Add("@a", SqlDbType.Int).Value = din.Text;
            cm.Parameters.Add("@b", SqlDbType.Int).Value = Pana.Text;
            sql.openConnection();
            cm.ExecuteNonQuery();
            sql.closeConnection();
            

        }

        private void TabItem_MouseEnter_1(object sender, MouseEventArgs e)
        {

        }

        private void TabItem_MouseEnter_2(object sender, MouseEventArgs e)
        {
            MSSQL sql = new MSSQL();
            auto autom = new auto();
            SqlCommand del = new SqlCommand("delete  from anul truncate table anul", sql.getConnection);
            sql.openConnection();
            del.ExecuteNonQuery();
            sql.closeConnection();
            SqlCommand cm = new SqlCommand("insert into anul select id,anul,tara from automobile ", sql.getConnection);
            sql.openConnection();
            cm.ExecuteNonQuery();
            sql.closeConnection();
            SqlCommand cmd = new SqlCommand("select  anul,tara from anul group by anul, tara", sql.getConnection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("an");
            sda.Fill(dt);
            ant.ItemsSource = dt.DefaultView;
            ant.IsReadOnly = true;
        }

        private void perioadalista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Modifica_Click_1(object sender, RoutedEventArgs e)
        {
            bool verif()
            {
                if ((model1.Text.Trim() == "") ||
                    (Culoarea1.Text.Trim() == "") ||
                    (tarap1.Text.Trim() == "") ||
                    (Capmot1.Text.Trim() == "") ||
                    (Pret1.Text.Trim() == "") ||
                    (Marca1.Text.Trim() == "") ||
                    (Anulp1.Text.Trim() == "") ||
                    (Transmisie1.Text.Trim() == ""))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            auto autom = new auto();
            int id = Convert.ToInt32(ID.Text);
            string mr = Marca1.Text;
            string md = model1.Text;
            string cl = Culoarea1.Text;
            string tara = tarap1.Text;
            int anul = Int32.Parse(Anulp1.Text);
            string conb = "Benzina";
            if (Dizel1.IsChecked == true)
            {
                conb = "Disel";
            }

            double cap = Convert.ToDouble(Capmot1.Text);
            string trans = Transmisie1.Text;
            int pret = Convert.ToInt32(Pret1.Text);
            string starea = Starea.Text;

            if (verif())
            {
                if (autom.updateAuto(id, mr, md, cl, tara, anul, conb, cap, trans, pret, starea))
                {
                    MessageBox.Show("Automobil Modificat", "Automobil Modifica", MessageBoxButton.OK, MessageBoxImage.Information);
                    ID.Text = "";
                    Marca1.Text = "";
                    model1.Text = "";
                    tarap1.Text = "";
                    Anulp1.Text = "";
                    Culoarea1.Text = "";
                    Capmot1.Text = "";
                    Transmisie1.Text = "";
                    Pret1.Text = "";
                    Starea.Text = "";
                }
                else
                {
                    MessageBox.Show("Eroare", "Automobil Modifica", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Camp Gol ", "Automobil Modifica", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void pc_Click(object sender, RoutedEventArgs e)
        {
            auto autom = new auto();
            MSSQL sql = new MSSQL();

            switch (perioadalista.SelectedIndex)
            {
                case 0:
                    SqlCommand cmd = new SqlCommand("select marca, model,culoare,tara,anul,conb,cap,trans,DataProcurarii from cumparatori inner join automobile on automobile.id=cumparatori.idauto where DataProcurarii > dateadd(m, -3, getdate() - datepart(d, getdate()) + 1) ", sql.getConnection);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("vinanzari");
                    sda.Fill(dt);
                    vanzari.ItemsSource = dt.DefaultView;
                    vanzari.IsReadOnly = true;

                    break;
                case 1:
                    SqlCommand cmd1 = new SqlCommand("select marca, model,culoare,tara,anul,conb,cap,trans,DataProcurarii from cumparatori inner join automobile on automobile.id=cumparatori.idauto where DataProcurarii > dateadd(m, -6, getdate() - datepart(d, getdate()) + 1) ", sql.getConnection);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("vinanzari");
                    sda1.Fill(dt1);
                    vanzari.ItemsSource = dt1.DefaultView;
                    vanzari.IsReadOnly = true;
                    break;
                case 2:
                    SqlCommand cmd2 = new SqlCommand("select marca, model,culoare,tara,anul,conb,cap,trans,DataProcurarii from cumparatori inner join automobile on automobile.id=cumparatori.idauto where DataProcurarii > dateadd(m, -12, getdate() - datepart(d, getdate()) + 1) ", sql.getConnection);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("vinanzari");
                    sda2.Fill(dt2);
                    vanzari.ItemsSource = dt2.DefaultView;
                    vanzari.IsReadOnly = true;
                    break;
                default:

                    break;
            }
        }

        private void mr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            auto autom = new auto();
            MSSQL sql = new MSSQL();
            SqlCommand cmd = new SqlCommand("select Id,marca,model,culoare,tara,anul,conb,cap,trans,min(pret), max(pret),starea from automobile where tara=@ta and culoare=@cl and starea='In Lot' group by Id,marca,model,culoare,tara,anul,conb,cap,trans,starea", sql.getConnection);
            cmd.Parameters.Add("@cl", SqlDbType.VarChar).Value = mr.SelectedItem;
            cmd.Parameters.Add("@ta", SqlDbType.VarChar).Value = ta.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable at = new DataTable("auto");
            sda.Fill(at);
            Lista.ItemsSource = at.DefaultView;
            Lista.IsReadOnly = true;

        }

        private void ta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

            MSSQL sql = new MSSQL();

            SqlCommand Cmd1 = new SqlCommand("SELECT DISTINCT tara FROM automobile ", sql.getConnection);
            sql.openConnection();
            SqlDataReader sqlReader = Cmd1.ExecuteReader();

            while (sqlReader.Read())
            {
                taralista.Items.Add(sqlReader["tara"].ToString());

            }
            sqlReader.Close();

            SqlCommand Cmd2 = new SqlCommand("SELECT DISTINCT tara FROM automobile ", sql.getConnection);
            sql.openConnection();
            SqlDataReader sqlReader2 = Cmd2.ExecuteReader();

            while (sqlReader2.Read())
            {

                ta.Items.Add(sqlReader2["tara"].ToString());
            }

            sqlReader2.Close();

            SqlCommand Cmd3 = new SqlCommand("SELECT DISTINCT culoare FROM automobile ", sql.getConnection);

            sql.openConnection();
            SqlDataReader sqlReader3 = Cmd3.ExecuteReader();

            while (sqlReader3.Read())
            {
                mr.Items.Add(sqlReader3["culoare"].ToString());
            }

            sqlReader3.Close();


            



        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            MSSQL sql = new MSSQL();
            auto autom = new auto();
            SqlCommand cmd = new SqlCommand("select id,marca,model,culoare,tara,anul,conb,cap,trans,pret,starea from automobile where pret > 50000 ", sql.getConnection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("automobile");
            sda.Fill(dt);

            DataSet ds = new DataSet("New_DataSet");
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            ds.Tables.Add(dt);
            ExcelLibrary.DataSetHelper.CreateWorkbook("exel.xls", ds);



        }

        private void TabItem_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Starea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cumparator cum = new Cumparator();
            switch (Starea.SelectedIndex)
            {
                case 0:
                    cum.Hide();

                    break;
                case 1:
                    cum.Show();
                    cum.id.Text = ID.Text; 
                    break;
                default:

                    break;
            }
        }

        private DataTable dt1 = new DataTable();

        public DataTable data
        {
            get {
                MSSQL sql = new MSSQL();
                SqlCommand cmd4 = new SqlCommand("select marca,count(*) as nor from  automobile group by marca having count(*) = (select max(nor) from (select marca, count(*) as nor from automobile group by marca) automobile )", sql.getConnection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                
                sda.Fill(dt1);
                return dt1;

            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


