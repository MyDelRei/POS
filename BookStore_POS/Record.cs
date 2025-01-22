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
    public partial class Record : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public Record()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }
        public void Topsale()
        {

            try
            {
                conn.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("SELECT TOP 10 pcode, pdes, SUM(qty) AS qty, SUM(total) AS total FROM vwDailySale GROUP BY pcode, pdes ORDER BY SUM(qty) DESC;", conn);
                da.Fill(dt);
                conn.Close();
                dgvRecord.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvRecord.Rows.Add(); // Add a new row
                    dgvRecord.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dgvRecord.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dgvRecord.Rows[rowIndex].Cells[2].Value = row[2];
                    dgvRecord.Rows[rowIndex].Cells[3].Value = row[3] + "$";

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void TodaySale()
        {

            try
            {
                conn.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("SELECT pcode,pdes,sum(qty)as qty,sum(total)  FROM vwDailySale WHERE sdate = CAST(GETDATE() AS DATE) group by pcode,pdes;", conn);
                da.Fill(dt);
                conn.Close();
                dgvRecord.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvRecord.Rows.Add(); // Add a new row
                    dgvRecord.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dgvRecord.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dgvRecord.Rows[rowIndex].Cells[2].Value = row[2];
                    dgvRecord.Rows[rowIndex].Cells[3].Value = row[3] + "$";

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void MonthlySale()
        {

            try
            {
                conn.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("SELECT  pcode,pdes,sum(qty)as qty,sum(total) FROM vwDailySale WHERE sdate >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0) AND sdate < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0) group by pcode,pdes;", conn);
                da.Fill(dt);
                conn.Close();
                dgvRecord.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvRecord.Rows.Add(); // Add a new row
                    dgvRecord.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dgvRecord.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dgvRecord.Rows[rowIndex].Cells[2].Value = row[2];
                    dgvRecord.Rows[rowIndex].Cells[3].Value = row[3] + "$";

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void Allitem()
        {

            try
            {
                conn.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("select pcode,pdes,sum(qty)as qty,sum(total) as total from vwDailySale group by pcode,pdes", conn);
                da.Fill(dt);
                conn.Close();
                dgvRecord.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvRecord.Rows.Add(); // Add a new row
                    dgvRecord.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dgvRecord.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dgvRecord.Rows[rowIndex].Cells[2].Value = row[2];
                    dgvRecord.Rows[rowIndex].Cells[3].Value = row[3] + "$";

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTopSale_Click(object sender, EventArgs e)
        {
            Topsale();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TodaySale();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MonthlySale();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Allitem();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    
}

