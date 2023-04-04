using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop.Word;


namespace MY_AIS_VKR
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class Documents : Form
    {
        OleDbConnection db = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=my_vkr.mdb");

        int selectedRow;

        public Documents()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView_stats.Columns.Add("id", "ID");
            dataGridView_stats.Columns.Add("title_stats", "Название");
            dataGridView_stats.Columns.Add("date_stats", "Дата");
            dataGridView_stats.Columns.Add("IsNew", String.Empty);
        }

        private void ClearFields()
        {
            textBox_id_stats.Text = "";
            textBox_title_stats.Text = "";
            textBox_date_stats.Text = "";
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from stats";

            OleDbCommand command = new OleDbCommand(queryString, db);

            db.Open();

            OleDbDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            db.Close();
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = "select * from stats where (title_stats) like '%" + textBox_search_stats.Text + "%'";

            OleDbCommand command = new OleDbCommand(searchString, db);

            db.Open();

            OleDbDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
            db.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView_stats.CurrentCell.RowIndex;

            dataGridView_stats.Rows[index].Visible = false;

            if (dataGridView_stats.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView_stats.Rows[index].Cells[3].Value = RowState.Deleted;
                return;
            }

            dataGridView_stats.Rows[index].Cells[3].Value = RowState.Deleted;
        }

        private new void Update()
        {
            db.Open();

            for (int index = 0; index < dataGridView_stats.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView_stats.Rows[index].Cells[3].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView_stats.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from stats where id = {id}";

                    OleDbCommand command = new OleDbCommand(deleteQuery, db);
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView_stats.Rows[index].Cells[0].Value.ToString();
                    var title = dataGridView_stats.Rows[index].Cells[1].Value.ToString();
                    var date = dataGridView_stats.Rows[index].Cells[2].Value.ToString();

                    var changeQuery = $"update stats set title_stats = '{title}', date_stats = '{date}' where id = '{id}'";

                    var command = new OleDbCommand(changeQuery, db);
                    command.ExecuteNonQuery();
                }
            }

            db.Close();
        }

        private void Change()
        {
            int selectedRowIndex = dataGridView_stats.CurrentCell.RowIndex;

            string id = textBox_id_stats.Text;
            string title = textBox_title_stats.Text;
            string date = textBox_date_stats.Text;

            if (dataGridView_stats.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView_stats.Rows[selectedRowIndex].SetValues(id, title, date);
                dataGridView_stats.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mf = new MainForm();
            mf.ShowDialog();
        }

        private void Documents_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.contracts". При необходимости она может быть перемещена или удалена.
            //this.contractsTableAdapter.Fill(this.my_vkrDataSet.contracts);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "my_vkrDataSet.orders". При необходимости она может быть перемещена или удалена.
            //this.ordersTableAdapter.Fill(this.my_vkrDataSet.orders);
            
            CreateColumns();
            RefreshDataGrid(dataGridView_stats);

            this.dataGridView_stats.Columns["IsNew"].Visible = false;
        }

        private void dataGridView_stats_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_stats.Rows[selectedRow];

                textBox_id_stats.Text = row.Cells[0].Value.ToString();
                textBox_title_stats.Text = row.Cells[1].Value.ToString();
                textBox_date_stats.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btn_add_stats_Click(object sender, EventArgs e)
        {
            db.Open();

            string title = textBox_title_stats.Text;
            string date = textBox_date_stats.Text;

            string addQuery = $"insert into stats(title_stats, date_stats) values('{title}', '{date}')";

            OleDbCommand command = new OleDbCommand(addQuery, db);

            if (command.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка добавления записи!", "Ошибка!");
            else
                MessageBox.Show("Запись успешно создана!!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            db.Close();
        }

        private void btn_refresh_stats_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView_stats);
            ClearFields();
        }

        private void textBox_search_stats_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView_stats);
        }

        private void btn_delete_stats_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }

        private void btn_change_stats_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void btn_save_stats_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void btn_clear_stats_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btn_Opendoc_stats_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string filepath = fileDialog.FileName.ToString();

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc = app.Documents.Open(filepath);
            string data = doc.Content.Text;
            OpenDocument open = new OpenDocument();
            open.richTextBox1.Text = data;
            open.ShowDialog();

            app.Quit();
        }

        
    }
}
