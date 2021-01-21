using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace ChuanDoanBenh
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Query query = new Query();
        public Form1()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ctlChuandoan ctlChuandoan = new ctlChuandoan();
            query.AddTabControl(xtraTabControl1, ctlChuandoan, "Chuẩn đoán bệnh");
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ctlAddBenh addBenh = new ctlAddBenh();
            query.AddTabControl(xtraTabControl1, addBenh, "Quản lý bệnh trong hệ thống");
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            ctlTrieuchung ctlTrieuchung = new ctlTrieuchung();
            query.AddTabControl(xtraTabControl1, ctlTrieuchung, "Quản lý triệu chứng trong hệ thống");
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            int index = xtraTabControl1.SelectedTabPageIndex;
            xtraTabControl1.TabPages.RemoveAt(index);
            if (index >= 1)
                xtraTabControl1.SelectedTabPageIndex = index - 1;
        }

        private void xtraTabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
        }
    }
}