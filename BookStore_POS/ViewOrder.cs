using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore_POS
{
    public partial class ViewOrder : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string sittle = "simple pos system";
        SqlDataAdapter da;
        DataTable dt;
        public ViewOrder()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void load_cart_order()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter("select transNo ,sum(qty),sum(total) ,sum(disc),status from cart group by transNo,status", conn);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvDailySales.Rows.Add();
                    dgvDailySales.Rows[rowIndex].Cells[0].Value = row[0];
                    dgvDailySales.Rows[rowIndex].Cells[1].Value = row[1];
                    dgvDailySales.Rows[rowIndex].Cells[2].Value = row[2]+"$";
                    dgvDailySales.Rows[rowIndex].Cells[3].Value = row[3]+"$";
                    dgvDailySales.Rows[rowIndex].Cells[4].Value = row[4];
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                dt = null;
            }
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {
            load_cart_order();
        }
    }
}
