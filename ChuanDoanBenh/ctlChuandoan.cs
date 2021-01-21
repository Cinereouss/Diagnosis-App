using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChuanDoanBenh
{
    public partial class ctlChuandoan : UserControl
    {
        Query query = new Query();
        private DataTable dataBenh = new DataTable();
        private DataTable dataTrieuchung = new DataTable();
        private DataTable dataChuandoan = new DataTable();
        string idNow = "";
        int pointTT = 0;
        
        private DataTable dataTrieuchungbenh = new DataTable();
        public ctlChuandoan()
        {
            InitializeComponent();
        }

        private string GetCauHoi(int vt)
        {
            return dataTrieuchungbenh.Rows[vt][1].ToString();
        }
        
        private void ctlChuandoan_Load(object sender, EventArgs e)
        {
            txtCTTN.Enabled = true;
            txtCTTN.Clear();
            txtMain.Clear();
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = true;
            button1.Enabled = true;
            dataBenh = query.GetAll("benh");
            dataTrieuchung = query.GetAll("trieuchung");
            dataChuandoan = null;
            for (int i = 0; i < dataTrieuchung.Rows.Count; i++)
            {
                txtCTTN.AutoCompleteCustomSource.Add(dataTrieuchung.Rows[i][1].ToString());
            }
            for (int i = 0; i < dataBenh.Rows.Count; i++)
            {
                comboBox1.AutoCompleteCustomSource.Add(dataBenh.Rows[i][1].ToString());
                comboBox1.Items.Add(dataBenh.Rows[i][1].ToString());
            }
            comboBox1.Text = "Lựa chọn bệnh................";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                string expression = "Tenbenh = '" + comboBox1.Text+"'";
                DataRow[] row = dataBenh.Select(expression);
                txtDT.Text = row[0][2].ToString();
                DataTable dataTableTemp = query.GetAllTrieuchung(row[0][0].ToString());
                for (int i = 0; i < dataTableTemp.Rows.Count; i++)
                {
                    txtTC.Text += dataTableTemp.Rows[i][1].ToString() + "\r\n";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            txtCTTN.Enabled = false;
            dataChuandoan = query.GetIdBenh(txtCTTN.Text);
            dataGridView1.DataSource = dataChuandoan;
            idNow = dataGridView1.Rows[0].Cells[0].Value.ToString();
            dataTrieuchungbenh = query.GetAllCauhoi(idNow);
            if (GetCauHoi(pointTT) == "Bạn có cảm thấy " + txtCTTN.Text + " không?")
            {
                pointTT++;
            }
            txtMain.Text = GetCauHoi(pointTT);
        }

        private void proc(int vt)
        {
            string name = dataTrieuchungbenh.Rows[vt][0].ToString();
            int n = dataChuandoan.Rows.Count;
            for (int i = 1; i < n; i++)
            {
                DataTable tempTC = query.GetAllTrieuchung(dataChuandoan.Rows[i][0].ToString());
                bool isYes = false;
                for (int j = 0; j < tempTC.Rows.Count; j++)
                {
                    if(tempTC.Rows[j][0].ToString() == name)
                    {
                        isYes = true;
                    }
                }
                if(isYes == false)
                {
                    dataChuandoan.Rows.RemoveAt(i);
                    n = n - 1;
                    i = i - 1;
                }
            }
            dataGridView1.DataSource = dataChuandoan;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pointTT++;
            if (pointTT < dataTrieuchungbenh.Rows.Count)
            {
                if (GetCauHoi(pointTT) == "Bạn có cảm thấy " + txtCTTN.Text + " không?")
                {
                    simpleButton2_Click(sender, e);
                }
                else
                {
                    proc(pointTT);
                    txtMain.Text = GetCauHoi(pointTT);
                }
                if (dataGridView1.Rows.Count == 1 && pointTT == dataTrieuchungbenh.Rows.Count)
                {
                    txtMain.Text = "Bạn đã mắc phải bệnh " + dataGridView1.Rows[0].Cells[1].Value.ToString();
                    simpleButton1.Enabled = false;
                    simpleButton2.Enabled = false;
                }
            }
            else
            {
                txtMain.Text = "Bạn đã mắc phải bệnh " + dataGridView1.Rows[0].Cells[1].Value.ToString();
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dataChuandoan.Rows.RemoveAt(0);
            pointTT = 0;
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Bệnh này không có trong hệ thống, vui lòng liên hệ bác sĩ","Hệ thống thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ctlChuandoan_Load(sender, e);
            }
            else
            {
                idNow = dataGridView1.Rows[0].Cells[0].Value.ToString();
                dataTrieuchungbenh = query.GetAllCauhoi(idNow);
                if (GetCauHoi(pointTT) == "Bạn có cảm thấy " + txtCTTN.Text + " không?")
                {
                    pointTT++;
                }
                txtMain.Text = GetCauHoi(pointTT);
            }
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ctlChuandoan_Load(sender, e);
        }
    }
}
