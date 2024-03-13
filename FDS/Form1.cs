using FDS.Components;
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
using static FDS.Components.Widgets;

namespace FDS
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARKANI\SQLEXPRESS;Initial Catalog=fds;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        public void AddControls(Form f)
        {
            flowLayoutPanel1.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            flowLayoutPanel1.Controls.Add(f);
            f.Show();
        }
        public void AddItem(string name, double cost, Categories category, Image icon)
        {
            var newWidget = new Components.Widgets()
            {
                Title = name,
                Cost = cost,
                Category = category,
                Icon = icon
            };

            flowLayoutPanel1.Controls.Add(newWidget);
        }
        private double totalCost = 0.0;
        private void AddWidgetToGrid(object sender, EventArgs e)
        {
            Widgets widget = (Widgets)sender;

            // Retrieve the name and cost from the clicked widget
            string name = widget.Title;
            double cost = widget.Cost;

            // Add the name and cost to the DataGridView
            Grid.Rows.Add(name, cost);

            // Update the total cost
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            totalCost = 0.0;

            foreach (DataGridViewRow row in Grid.Rows)
            {
                if (row.Cells["Cost"].Value != null)
                {
                    double cost = 0.0;
                    if (double.TryParse(row.Cells["Cost"].Value.ToString(), out cost))
                    {
                        totalCost += cost;
                    }
                }
            }

            lbltotal.Text = totalCost.ToString("c2");
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Display();
        }
        public void Display()
        {

            // Assuming you have a database connection named "conn"

            try
            {
                conn.Open();

                string query = "SELECT Name, Price, Image FROM Pro";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int price = reader.GetInt32(1);
                            byte[] imageData = (byte[])reader["Image"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                AddItem(name, price, Categories.Food, image);
                                // Replace Categories.Food with the appropriate category for each item
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            foreach (Widgets widget in flowLayoutPanel1.Controls.OfType<Widgets>())
            {
                widget.OnSelect += AddWidgetToGrid;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnclearall_Click(object sender, EventArgs e)
        {
            Grid.Rows.Clear();
            CalculateTotalCost();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            CatogeryAdd catt = new CatogeryAdd();
            catt.ShowDialog();
        }
        private void LoadProductData()
        {
            try
            {
                conn.Open();

                string query = "SELECT Name, Price, Image FROM Pro";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing product data
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int price = reader.GetInt32(1);
                            byte[] imageData = (byte[])reader["Image"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                AddItem(name, price, Categories.Food, image);
                                // Replace Categories.Food with the appropriate category for each item
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnAddProd_Click(object sender, EventArgs e)
        {
            AddProducts pro = new AddProducts();
            pro.ShowDialog();


        }

        private void btnallitems_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query = "SELECT Name, Price, Image FROM Pro";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing product data
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int price = reader.GetInt32(1);
                            byte[] imageData = (byte[])reader["Image"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                AddItem(name, price, Categories.Food, image);
                                // Replace Categories.Food with the appropriate category for each item
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            foreach (Widgets widget in flowLayoutPanel1.Controls.OfType<Widgets>())
            {
                widget.OnSelect += AddWidgetToGrid;
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            String labelValue = lbltotal.Text;
            Delivery delivery = new Delivery();
            delivery.LabelValue = labelValue;

            delivery.ShowDialog();
            Grid.Rows.Clear();
            CalculateTotalCost();
        }
        public String catname = "Food";
        public void Food_Click(object sender, EventArgs e)
        {
            
            try
            {
                conn.Open();

                string query = "SELECT Name, Price, Image FROM Pro where CategoryName = @Categoryname";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Categoryname", catname);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing product data
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int price = reader.GetInt32(1);
                            byte[] imageData = (byte[])reader["Image"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                AddItem(name, price, Categories.Food, image);
                                // Replace Categories.Food with the appropriate category for each item
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            foreach (Widgets widget in flowLayoutPanel1.Controls.OfType<Widgets>())
            {
                widget.OnSelect += AddWidgetToGrid;
            }
        }
        public String DrinkName = "Drink";
        private void ColdDrinks_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query = "SELECT Name, Price, Image FROM Pro where CategoryName = @Categoryname";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Categoryname", DrinkName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing product data
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int price = reader.GetInt32(1);
                            byte[] imageData = (byte[])reader["Image"];

                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image image = Image.FromStream(ms);
                                AddItem(name, price, Categories.Food, image);
                                // Replace Categories.Food with the appropriate category for each item
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            foreach (Widgets widget in flowLayoutPanel1.Controls.OfType<Widgets>())
            {
                widget.OnSelect += AddWidgetToGrid;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            CategoryList CL = new CategoryList();
            CL.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ProductList p = new ProductList();
            p.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Package pk = new Package();
            pk.ShowDialog();
        }
    }
}
