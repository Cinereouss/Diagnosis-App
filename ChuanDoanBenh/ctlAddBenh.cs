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
    public partial class ctlAddBenh : UserControl
    {
        Query query = new Query();
        private DataTable data, data2, data2temp, databenh;
        string idSelected = "";

        public ctlAddBenh()
        {
            InitializeComponent();
        }

        private void ctlAddBenh_Load(object sender, EventArgs e)
        {


            textBox1.Clear();
            textBox2.Clear();
            databenh = query.GetAll("benh");
            dataGridView1.DataSource = databenh;
            data = query.GetTT();
            listTT.DataSource = data;
            idSelected = "";

        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (listTT.SelectedRows.Count > 0)
            {
                DataGridViewRow row = listTT.SelectedRows[0];
                DataRow dataRow = data2.NewRow();
                dataRow[0] = row.Cells[0].Value.ToString();
                dataRow[1] = row.Cells[1].Value.ToString();
                data2.Rows.Add(dataRow);
                //data2.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), "", "");
                listTT.Rows.RemoveAt(this.listTT.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn triệu chứng", "List Triệu chứng thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            listTTcuaBenh.DataSource = data2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listTTcuaBenh.SelectedRows.Count > 0)
            {
                DataGridViewRow row = listTTcuaBenh.SelectedRows[0];
                data.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                listTTcuaBenh.Rows.RemoveAt(this.listTTcuaBenh.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn triệu chứng", "List Triệu chứng thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            while (listTTcuaBenh.Rows.Count > 0)
            {
                listTTcuaBenh.Rows.RemoveAt(0);
            }
            data = query.GetAll("trieuchung");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa bệnh " + textBox1.Text + " ra khỏi hệ thống không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query.DeleteChuandoan(idSelected);
                query.DeleteFormTable(idSelected, "benh");
                ctlAddBenh_Load(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (listTTcuaBenh.Rows.Count > 0)
            {
                query.AddBenh(textBox1.Text, textBox2.Text);
                float tile = (float)1 / (float)listTTcuaBenh.Rows.Count;
                int idbenh = query.GetIdNewRecord("benh");
                for (int i = 0; i < listTTcuaBenh.Rows.Count; i++)
                {
                    DataGridViewRow row = listTTcuaBenh.Rows[i];
                    query.Addchuandoan(idbenh.ToString(), row.Cells[0].Value.ToString(), tile.ToString());
                }
                MessageBox.Show("Bệnh " + textBox1.Text + " đã được thêm vào hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnHuy_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn triệu chứng");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1) return;
            else
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                idSelected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                data2 = query.GetAllTrieuchung(idSelected);
                listTTcuaBenh.DataSource = data2;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            layoutControlGroup3.Enabled = false;
            layoutControlGroup4.Enabled = false;
            textBox1.Enabled = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            listTT.Enabled = false;
            listTTcuaBenh.Enabled = false;
            ctlAddBenh_Load(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            layoutControlGroup3.Enabled = true;
            layoutControlGroup4.Enabled = true;
            textBox1.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            listTT.Enabled = true;
            listTTcuaBenh.Enabled = true;
            data2.Clear();
            listTTcuaBenh.DataSource = data2;
        }
    }
}
