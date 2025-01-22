using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore_POS
{
    public partial class Discount : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string sittle = "simple pos system";
        POS f;
        public Discount(POS frm)
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            f = frm;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void discount_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("do you want to add this discount ? ", sittle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("update cart set disc=@discount where transNo = @transNo", conn);
                    cmd.Parameters.AddWithValue("@discount",txtDiscountTotal.Text);
                    cmd.Parameters.AddWithValue("@transNo", f.lblTransNo.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("success");
                    f.loadCart();
                    Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void Discount_Load_1(object sender, EventArgs e)
        {
            txtPrice.Text = f.lblTotal.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                double discount = double.Parse(txtPrice.Text) * double.Parse(txtDiscount.Text);
                txtDiscountTotal.Text = discount.ToString();
            }
            catch (Exception ex)
            {
                txtDiscountTotal.Text = "0.00";
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }

}
