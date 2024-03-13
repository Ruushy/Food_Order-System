namespace FDS.Components
{
    partial class Widgets
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.imageicon = new System.Windows.Forms.PictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lbcost = new System.Windows.Forms.Label();
            this.lbltitle = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageicon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CustomGradientPanel1.BorderColor = System.Drawing.Color.Silver;
            this.guna2CustomGradientPanel1.BorderRadius = 100;
            this.guna2CustomGradientPanel1.Controls.Add(this.imageicon);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2PictureBox1);
            this.guna2CustomGradientPanel1.Controls.Add(this.lbcost);
            this.guna2CustomGradientPanel1.Controls.Add(this.lbltitle);
            this.guna2CustomGradientPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(3, 3);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(258, 182);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            this.guna2CustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel1_Paint);
            // 
            // imageicon
            // 
            this.imageicon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageicon.BackColor = System.Drawing.Color.Transparent;
            this.imageicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageicon.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.imageicon.Image = global::FDS.Properties.Resources.fast1;
            this.imageicon.Location = new System.Drawing.Point(130, 18);
            this.imageicon.Name = "imageicon";
            this.imageicon.Size = new System.Drawing.Size(125, 161);
            this.imageicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageicon.TabIndex = 3;
            this.imageicon.TabStop = false;
            this.imageicon.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(122, 55);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(87, 58);
            this.guna2PictureBox1.TabIndex = 2;
            this.guna2PictureBox1.TabStop = false;
            // 
            // lbcost
            // 
            this.lbcost.AutoSize = true;
            this.lbcost.Font = new System.Drawing.Font("Microsoft YaHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcost.ForeColor = System.Drawing.Color.Gold;
            this.lbcost.Location = new System.Drawing.Point(13, 125);
            this.lbcost.Name = "lbcost";
            this.lbcost.Size = new System.Drawing.Size(55, 22);
            this.lbcost.TabIndex = 1;
            this.lbcost.Text = "$1.54";
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(3, 0);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(65, 22);
            this.lbltitle.TabIndex = 0;
            this.lbltitle.Text = "Burger";
            // 
            // Widgets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Name = "Widgets";
            this.Size = new System.Drawing.Size(273, 188);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageicon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        public System.Windows.Forms.PictureBox imageicon;
        public System.Windows.Forms.Label lbcost;
        public System.Windows.Forms.Label lbltitle;
    }
}
