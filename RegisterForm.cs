using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MY_AIS_VKR
{
    public partial class RegisterForm : Form
    {

        DataBase db = new DataBase();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox_login.Text = "";
            textBox_password.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
            textBox_password.UseSystemPasswordChar = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
            textBox_password.UseSystemPasswordChar = true;
        }

        private void btnLog_in_Click(object sender, EventArgs e)
        {
            string login = textBox_login.Text;
            string password = textBox_password.Text;

            string querystring = $"insert into users(login_user, password_user) values ('{login}', '{password}')";

            OleDbConnection dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=my_vkr.mdb");

            OleDbCommand command = new OleDbCommand(querystring, dbConnection);

            if (checkuser())
            {
                return;
            }

            dbConnection.Open();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }

            dbConnection.Close();

            /* sql managment studio
            string login = textBox_login.Text;
            string password = textBox_password.Text;

            string querystring = $"insert into register(login_user, password_user) values ('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystring, db.GetConnection());

            if (checkuser())
            {
                return;
            }

            db.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }

            db.CloseConnection();
            */
        }

        private Boolean checkuser()
        {
            string loginUser = textBox_login.Text;
            string passUser = textBox_password.Text;

            OleDbConnection dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=my_vkr.mdb");

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();

            string query = $"select * from users where login_user = '{loginUser}' and password_user = '{passUser}'";

            OleDbCommand command = new OleDbCommand(query, dbConnection);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }

            /* sql managment studio
            string loginUser = textBox_login.Text;
            string passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"select * from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(query, db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
            */
        }

    }
}
