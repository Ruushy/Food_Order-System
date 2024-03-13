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
using System.IO;
namespace FDS
{
   
    public partial class UpdateProduct : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");

        public event Action ProductAdded;
       // public AddProducts();
        private string ID;
        private string categoryName;
        private string name;
        private string image;
        private string price;
        private SqlConnection connn;
        public UpdateProduct(string id, string category, string productName, string productImage, string productPrice, SqlConnection connection)
        {
            InitializeComponent();
            PopulateCategories();
            ID = id;
            categoryName = category;
            name = productName;
            image = productImage;
            price = productPrice;
            connn = connection;
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


        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            PopulateCategories();
            txtProductName.Text = name;
            txtProductPrice.Text = price;
            int categoryIndex = cmbProductCategory.FindStringExact(categoryName);
            if (categoryIndex != -1)
            {
                cmbProductCategory.SelectedIndex = categoryIndex;
            }
            else
            {
                // The category name doesn't match any existing item in the ComboBox
                // You can handle this case accordingly, such as displaying an error message
            }
           // Image backgroundImage = Image.FromFile(image);
           // browsePanel.BackgroundImage = backgroundImage;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
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

            // Update the data in the "Pro" table in the database
            try
            {
                conn.Open();

                string query = "UPDATE Pro SET CategoryName = @CategoryName, Price = @Price, Image = @ImagePath WHERE Name = @Name";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", cmbProductCategory.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@Name", txtProductName.Text);
                    command.Parameters.AddWithValue("@Price", decimal.Parse(txtProductPrice.Text));
                    byte[] imageBytes = File.ReadAllBytes(browsePanel.Text);
                    command.Parameters.AddWithValue("@ImagePath", imageBytes);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Data updated successfully.");
                //Form1 f = new Form1();
                //f.Display();
                // ProductAdded?.Invoke();
                txtProductName.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the data: " + ex.Message);
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
