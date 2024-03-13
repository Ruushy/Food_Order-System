using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FDS
{
    public partial class AddProducts : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");
        public event Action ProductAdded;
        public AddProducts()
        {
            InitializeComponent();
            PopulateCategories();
        }
        private void PopulateCategories()
        {
            // Clear existing items in the ComboBox
            cmbProductCategory.Items.Clear();

            // Fetch categories from the database and populate the ComboBox
            using (SqlCommand command = new SqlCommand("SELECT DISTINCT name FROM category", conn))
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string category = reader.GetString(0);
                    cmbProductCategory.Items.Add(category);
                }

                conn.Close();
            }


        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            //f.Form1_Load();
        }

        private void AddProducts_Load(object sender, EventArgs e)
        {
            PopulateCategories();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            // Validate that the required fields are filled
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtProductPrice.Text) ||
                string.IsNullOrWhiteSpace(cmbProductCategory.Text) ||
                string.IsNullOrWhiteSpace(browsePanel.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            // Parse the price value
            if (!double.TryParse(txtProductPrice.Text, out double price))
            {
                MessageBox.Show("Invalid price value.");
                return;
            }

            // Store the data to the "Products" table in the database
            try
            {
                conn.Open();

                string query = "INSERT INTO Pro (CategoryName, Name, Price, Image) VALUES (@CategoryName, @Name, @Price, @ImagePath)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", cmbProductCategory.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Name", txtProductName.Text);
                    command.Parameters.AddWithValue("@Price", decimal.Parse(txtProductPrice.Text));
                    byte[] imageBytes = File.ReadAllBytes(browsePanel.Text);
                    command.Parameters.AddWithValue("@ImagePath", imageBytes);
                    

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data saved successfully.");
                Form1 f = new Form1();
                f.Display();
                ProductAdded?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnProductBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                browsePanel.Text = openFileDialog.FileName;
            }
        }
    }
}
