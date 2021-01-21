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
    public partial class ctlTrieuchung : UserControl
    {
        int choose;
        string idSelected;
        Query query = new Query();
        public ctlTrieuchung()
        {
            InitializeComponent();
        }

        private void ctlTrieuchung_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = query.GetAll("trieuchung");
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                idSelected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Clear();
            layoutControlGroup3.Enabled = true;
            textBox2.Clear();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            choose = 1;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            layoutControlGroup3.Enabled = false;
            textBox1.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnHuy.Enabled = false;
            choose = 0;
            ctlTrieuchung_Load(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            layoutControlGroup3.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            choose = 2;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(choose == 1)
            {
                query.AddTrieuchung(textBox1.Text, textBox2.Text);
            }
            else
            {
                query.UpdateTrieuchung(idSelected, textBox1.Text, textBox2.Text);
            }
            btnHuy_Click(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xóa triệu chứng "+textBox1.Text+" ra khỏi hệ thống không?", "Thống báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query.DeleteFormTable(idSelected, "trieuchung");
                ctlTrieuchung_Load(sender, e);
            }
        }
    }
}
