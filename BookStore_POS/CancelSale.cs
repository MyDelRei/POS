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
    public partial class CancelSale : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        public CancelSale()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void frmCancelSale_Load(object sender, EventArgs e)
        {
            loadVwSale();
        }
        public void loadVwSale()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter("select *from vwDailySale", conn);
                da.Fill(dt);
                conn.Close();
                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add(); // Add a new row
                    dataGridView1.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dataGridView1.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dataGridView1.Rows[rowIndex].Cells[2].Value = row[2]; // Column 3
                    dataGridView1.Rows[rowIndex].Cells[3].Value = row[3]; // Column 4
                    dataGridView1.Rows[rowIndex].Cells[4].Value = row[4]; // Column 5
                    dataGridView1.Rows[rowIndex].Cells[5].Value = row[5]; // Column 6
                    dataGridView1.Rows[rowIndex].Cells[6].Value = row[6]; // Column 7
                    dataGridView1.Rows[rowIndex].Cells[7].Value = row[7]; // Column 8
                    dataGridView1.Rows[rowIndex].Cells[8].Value = row[8]; // Column 9
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                da = new SqlDataAdapter("select *from vwDailySale where TransNo like '" + txtsearch.Text + "'", conn);
                da.Fill(dt);
                conn.Close();
                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add(); // Add a new row
                    dataGridView1.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dataGridView1.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dataGridView1.Rows[rowIndex].Cells[2].Value = row[2]; // Column 3
                    dataGridView1.Rows[rowIndex].Cells[3].Value = row[3]; // Column 4
                    dataGridView1.Rows[rowIndex].Cells[4].Value = row[4]; // Column 5
                    dataGridView1.Rows[rowIndex].Cells[5].Value = row[5]; // Column 6
                    dataGridView1.Rows[rowIndex].Cells[6].Value = row[6]; // Column 7
                    dataGridView1.Rows[rowIndex].Cells[7].Value = row[7]; // Column 8
                    dataGridView1.Rows[rowIndex].Cells[8].Value = row[8]; // Column 9
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cancelSale()
        {
            int i = 0;
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                conn.Open();
                cmd = new SqlCommand("update Product_tbl set qty = qty + " + int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) + "where pcode = @pcode", conn);
                cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[i].Cells[2].Value.ToString());
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Open();
                cmd = new SqlCommand("delete from cart where transNo like '" + txtsearch.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            MessageBox.Show("Cancel succeed"); Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cancelSale();
        }
    }
}
