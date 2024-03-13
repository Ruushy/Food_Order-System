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
namespace FDS
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string username = txtmagac.Text;
            string password = txtsir.Text;

            string query = "SELECT COUNT(*) FROM users WHERE username = @Username AND password = @Password";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                conn.Open();

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Login successful!");
                    Form1 db = new Form1();
                    db.ShowDialog();
                    this.Close();
                    // Perform any additional actions after successful login
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                    // Perform any actions for failed login attempt
                }

                conn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
