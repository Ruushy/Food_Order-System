using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FDS
{
    public partial class ProductList : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");
        private DataTable dataTable;

        public ProductList()
        {
            InitializeComponent();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT ID, CategoryName, Name, Price, Image FROM Pro";
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

            // Add edit button column
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                FlatStyle = FlatStyle.Flat,
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            editButtonColumn.DefaultCellStyle.BackColor = Color.FromArgb(255, 193, 7);
            editButtonColumn.DefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.Columns.Add(editButtonColumn);

            // Add delete button column
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                FlatStyle = FlatStyle.Flat,
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            deleteButtonColumn.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            deleteButtonColumn.DefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.Columns.Add(deleteButtonColumn);

            // Add image column
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                Name = "Image",
                HeaderText = "Image",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            guna2DataGridView1.Columns.Add(imageColumn);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Image" && e.RowIndex >= 0)
            {
                if (guna2DataGridView1.Rows[e.RowIndex].Cells["Image"].Value != null)
                {
                    byte[] imageData = (byte[])guna2DataGridView1.Rows[e.RowIndex].Cells["Image"].Value;
                    if (imageData.Length > 0)
                    {
                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            e.Value = Image.FromStream(stream);
                        }
                    }
                }
            }
        }
        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Handle the form closed event as needed
            // For example, you can refresh the data after the form is closed
            LoadData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    

    

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductList_Load_1(object sender, EventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                        string ID = selectedRow.Cells["ID"].Value.ToString();
                        string categoryName = selectedRow.Cells["CategoryName"].Value.ToString();
                        string name = selectedRow.Cells["Name"].Value.ToString();
                        string image = selectedRow.Cells["Image"].Value != null ? selectedRow.Cells["Image"].Value.ToString() : string.Empty;
                        string price = selectedRow.Cells["Price"].Value.ToString();

                        // Open the form for editing
                        UpdateProduct editForm = new UpdateProduct(ID, categoryName, name, image, price, conn);
                        editForm.FormClosed += EditForm_FormClosed;
                        editForm.ShowDialog(this);
                    }
                    else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        try
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                                string id = selectedRow.Cells["ID"].Value.ToString();
                                string query = "DELETE FROM Pro WHERE ID = @id";
                                using (SqlCommand command = new SqlCommand(query, conn))
                                {
                                    command.Parameters.AddWithValue("@id", id);

                                    conn.Open();

                                    int rowsAffected = command.ExecuteNonQuery();

                                    conn.Close();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Data deleted successfully!");
                                        LoadData();
                                        ConfigureGridView();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to delete data!");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
