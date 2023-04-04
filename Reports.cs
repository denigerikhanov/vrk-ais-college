using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY_AIS_VKR
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mf = new MainForm();
            mf.ShowDialog();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.accounting_reports". При необходимости она может быть перемещена или удалена.
            //this.accounting_reportsTableAdapter.Fill(this.my_vkrDataSet.accounting_reports);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.management_reports". При необходимости она может быть перемещена или удалена.
            //this.management_reportsTableAdapter.Fill(this.my_vkrDataSet.management_reports);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.tax_reports". При необходимости она может быть перемещена или удалена.
            //this.tax_reportsTableAdapter.Fill(this.my_vkrDataSet.tax_reports);

        }
    }
}
