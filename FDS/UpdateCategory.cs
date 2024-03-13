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
    public partial class UpdateCategory : Form
    {
        private string categoryId;
        private string categoryName;
        private SqlConnection connection;
        public UpdateCategory(string id, string name, SqlConnection conn)
        {
            InitializeComponent();
            categoryId = id;
            categoryName = name;
            connection = conn;



        }

        private void UpdateCategory_Load(object sender, EventArgs e)
        {
            txtName.Text = categoryName;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string newCategoryName = txtName.Text;

                // Update the category in the database
                string query = "UPDATE Category SET name = @name WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", newCategoryName);
                    command.Parameters.AddWithValue("@id", categoryId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Category updated successfully!");
                        Close(); // Close the form after the update is done
                    }
                    else
                    {
                        MessageBox.Show("Update failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
