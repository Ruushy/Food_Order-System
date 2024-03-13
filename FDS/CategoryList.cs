using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FDS
{
    public partial class CategoryList : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");
        private DataTable dataTable;

        public CategoryList()
        {
            InitializeComponent();
        }

        private void CategoryList_Load(object sender, EventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM Category";
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
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                        string ID = selectedRow.Cells["id"].Value.ToString();
                        string name = selectedRow.Cells["name"].Value.ToString();

                        UpdateCategory updateForm = new UpdateCategory(ID, name, conn);
                        updateForm.FormClosed += UpdateForm_FormClosed;
                        updateForm.ShowDialog(this);
                    }
                    else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        try
                        {
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                                string id = selectedRow.Cells["id"].Value.ToString();
                                string query = "DELETE FROM Category WHERE id = @id";
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
                                        MessageBox.Show("Deletion failed!");
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

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadData();
            ConfigureGridView();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}