using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FDS.Components
{
    public partial class Widgets : UserControl
    {
        public event EventHandler OnSelect = null;
        public Widgets()
        {
            InitializeComponent();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }
        public enum Categories { Food, ColdDrinks, HotDrinks, Desserts }

        private Categories _category;

        public Categories Category
        {
            get => _category;
            set
            {
                if (Enum.IsDefined(typeof(Categories), value))
                {
                    _category = value;
                }
                else
                {
                    throw new ArgumentException("Invalid category value.");
                }
            }
        }

        public string Title
        {
            get => lbltitle.Text;
            set => lbltitle.Text = value;
        }

        private double _Cost;

        public double Cost
        {
            get => _Cost;
            set
            {
                _Cost = value;
                lbcost.Text = _Cost.ToString("c2");
            }
        }

        public Image Icon
        {
            get => imageicon.Image;
            set => imageicon.Image = value;
        }
    }

}