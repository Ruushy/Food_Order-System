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
    public partial class Delivery : Form
    {
        public String LabelValue { get; set; }
        SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");

        public Delivery()
        {
            InitializeComponent();
            
            
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Package (name, email, address, phone_number, Total) VALUES (@FullName, @Email, @Address, @Phone, @Total)";
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);

            // Parse the total value
            if (!decimal.TryParse(txtTotal.Text.Replace("$", ""), out decimal total))
            {
                MessageBox.Show("Invalid total value.");
                return;
            }
            if (total == 0 || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please fill in all fields and ensure that the total is not zero.");
                return;
            }

            cmd.Parameters.AddWithValue("@Total", total);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data inserted successfully.");
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtPhone.Text = "";
                txtTotal.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Delivery_Load(object sender, EventArgs e)
        {
            txtTotal.Text = LabelValue;
        }
    }
}
