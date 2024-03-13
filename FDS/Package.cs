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
    public partial class Package : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");
        private DataTable dataTable;
        public Package()
        {
            InitializeComponent();
        }

        private void Package_Load(object sender, EventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM Package";
               
                dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ConfigureGridView()
        {
            guna2DataGridView1.DataSource = dataTable;

            // Configure grid view appearance
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
           
        }


        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
