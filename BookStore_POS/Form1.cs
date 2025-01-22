using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore_POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product frm = new Product();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stock frm = new Stock();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Record frm = new Record();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            User frm = new User();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS frm = new POS();
            frm.ShowDialog();
        }
    }
}
