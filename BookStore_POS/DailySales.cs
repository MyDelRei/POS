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
    public partial class DailySales : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string sittle = "simple pos system";
        public DailySales()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void DailySales_Load(object sender, EventArgs e)
        {
            dgvDailySales.Rows.Clear();
            LoadDailyProduct();

        }
        public void LoadDailyProduct()
        {
            dgvDailySales.Rows.Clear();
            try
            {
                int i = 0;
                conn.Open();
                cmd = new SqlCommand("select*from vwDailySale where sdate between '" + dtp1.Value + "'and '" + dtp2.Value + "' and status = 'sold'", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                    dgvDailySales.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }

        }
        public void SearchDailyProduct()
        {
            dgvDailySales.Rows.Clear();
            int i = 0;
            conn.Open();
            cmd = new SqlCommand("select*from vwDailySale where TransNo like '" + txtSearch.Text + "'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvDailySales.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
            }
            conn.Close();
        }

        private void load_Click(object sender, EventArgs e)
        {
            LoadDailyProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchDailyProduct();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    
}

