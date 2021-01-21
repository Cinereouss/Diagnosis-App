using DevExpress.XtraTab;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChuanDoanBenh
{
    class Query : Base
    {
        public Query() : base()
        {
        }
        private void AddingTabControl(XtraTabControl xtraTabParent, string xtraItem, UserControl userControl)
        {
            XtraTabPage xtraTabPage = new XtraTabPage();
            xtraTabPage.Text = xtraItem;
            xtraTabPage.Dock = DockStyle.Fill;
            xtraTabPage.Controls.Add(userControl);
            xtraTabParent.TabPages.Add(xtraTabPage);
            userControl.Dock = DockStyle.Fill;
        }

        public void AddTabControl(XtraTabControl xtraTabParent, UserControl userControl, string itemTabName)
        {
            bool isExists = false;
            foreach (XtraTabPage tabItem in xtraTabParent.TabPages)
            {
                if (tabItem.Text == itemTabName)
                {
                    isExists = true;
                    xtraTabParent.SelectedTabPage = tabItem;
                }
            }
            if (isExists == false)
            {
                AddingTabControl(xtraTabParent, itemTabName, userControl);
            }
        }

        public DataTable GetAll(string table)
        {
            string[] cols = { "*" };
            string[] conditions = { };
            MySqlParameter[] parameters = { };
            return Select(table, cols, conditions, parameters);
        }

        public DataTable GetTT()
        {
            string[] cols = { "Id", "Tentrieuchung"};
            string[] conditions = { };
            MySqlParameter[] parameters = { };
            return Select("trieuchung", cols, conditions, parameters);
        }

        public DataTable GetTenBenh()
        {
            string[] cols = {"Tenbenh" };
            string[] conditions = { };
            MySqlParameter[] parameters = { };
            return Select("benh", cols, conditions, parameters);
        }

        public DataTable GetIdBenh(string Trieuchung)
        {
            string[] cols = { "BenhId", "Tenbenh" };
            string[] conditions = { "Tentrieuchung = @Tentrieuchung"};
            MySqlParameter[] parameters = { new MySqlParameter("Tentrieuchung", Trieuchung) };
            return Select("trieuchung_benh", cols, conditions, parameters);
        }

        public DataTable GetAllTrieuchung(string benhid)
        {
            string[] cols = { "Id", "Tentrieuchung" };
            string[] conditions = { "Id = @Benhid"};
            MySqlParameter[] parameters = { new MySqlParameter("Benhid", benhid)  };
            return Select("benh_trieuchung", cols, conditions, parameters);
        }

        public DataTable GetAllCauhoi(string benhid)
        {
            string[] cols = {"Tentrieuchung", "Cauhoi" };
            string[] conditions = { "Id = @Benhid" };
            MySqlParameter[] parameters = { new MySqlParameter("Benhid", benhid) };
            return Select("benh_trieuchung", cols, conditions, parameters);
        }

        public int AddTrieuchung(string ten, string mota)
        {
            string[] cols = { "Tentrieuchung", "Mota", "Cauhoi"};
            string[] conditions = { "@Tentrieuchung", "@Mota", "@Cauhoi" };
            MySqlParameter[] parameters = { new MySqlParameter("Tentrieuchung", ten),
                                            new MySqlParameter("Mota", mota),
                                            new MySqlParameter("Cauhoi", "Bạn có cảm thấy " +ten+" không?")};
            return Insert("trieuchung ", cols, conditions, parameters);
        }

        public int UpdateTrieuchung(string Id, string ten, string mota)
        {
            string[] cols = { "Tentrieuchung = @Tentrieuchung", "Mota = @Mota", "Cauhoi = @Cauhoi" };
            string[] conditions = { "Id = @Id" };
            MySqlParameter[] parameters = { new MySqlParameter("Tentrieuchung", ten),
                                            new MySqlParameter("Mota", mota),
                                            new MySqlParameter("Cauhoi", "Bạn có cảm thấy " +ten+" không?"),
                                            new MySqlParameter("Id", Id)};

            return Update("trieuchung", cols, conditions, parameters);
        }
        public int DeleteFormTable(string Id, string table)
        {
            string[] conditions = { "Id = @Id" };
            MySqlParameter[] parameters = { new MySqlParameter("Id", Id) };
            return Delete(table, conditions, parameters);
        }

        public int DeleteChuandoan(string IdBenh)
        {
            string[] conditions = { "BenhId = @Id" };
            MySqlParameter[] parameters = { new MySqlParameter("Id", IdBenh) };
            return Delete("chuandoan", conditions, parameters);
        }

        public int AddBenh(string ten, string cdt)
        {
            string[] cols = { "Tenbenh", "Cachdieutri" };
            string[] conditions = { "@Tenbenh", "@Cachdieutri" };
            MySqlParameter[] parameters = { new MySqlParameter("Tenbenh", ten),
                                            new MySqlParameter("Cachdieutri", cdt)};
            return Insert("benh", cols, conditions, parameters);
        }


        public int Addchuandoan(string idbenh, string idtrieuchung, string tile)
        {
            string[] cols = { "BenhId", "TrieuchungId", "Tilebenh"};
            string[] conditions = { "@BenhId", "@TrieuchungId", "@Tilebenh" };
            MySqlParameter[] parameters = { new MySqlParameter("BenhId", idbenh),
                                            new MySqlParameter("TrieuchungId", idtrieuchung),
                                            new MySqlParameter("Tilebenh", tile)};
            return Insert("chuandoan", cols, conditions, parameters);
        }


        public int GetIdNewRecord(string table)
        {
            string[] cols = { "MAX(Id)" };
            string[] conditions = { };
            MySqlParameter[] parameters = { };
            DataTable dataTable = Select(table, cols, conditions, parameters);
            DataRow row = dataTable.Rows[0];
            return Convert.ToInt32(row[0].ToString());
        }
    }
}
