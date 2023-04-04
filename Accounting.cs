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
    public partial class Accounting : Form
    {
        public Accounting()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mf = new MainForm();
            mf.ShowDialog();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_accruals_Click(object sender, EventArgs e)
        {

        }

        private void Accounting_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.expenses". При необходимости она может быть перемещена или удалена.
            //this.expensesTableAdapter.Fill(this.my_vkrDataSet.expenses);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.holdings". При необходимости она может быть перемещена или удалена.
            //this.holdingsTableAdapter.Fill(this.my_vkrDataSet.holdings);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.accruals". При необходимости она может быть перемещена или удалена.
            //this.accrualsTableAdapter.Fill(this.my_vkrDataSet.accruals);

        }
    }
}
