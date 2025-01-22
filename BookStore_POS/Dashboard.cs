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
using System.Windows.Forms.DataVisualization.Charting;

namespace BookStore_POS
{
    public partial class Dashboard : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Dashboard()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    public string loadsale()
        {
            string total = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("select sum(total) as total from vwDailySale", conn);
                cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    total = dr[0].ToString();

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            return total;
        }
        public string loadStock()
        {
            string total = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("select sum(qty) as HANDED from stock where status = 'done'", conn);
                cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    total = dr[0].ToString();

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            return total;
        }
        public string dailysale()
        {
            string total = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("select sum(total) from vwDailySale where sdate = cast(GETDATE() as Date)", conn);
                cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    total = dr[0].ToString();

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            return total;
        }
        private void LoadChart()
        {
            // Clear any existing series
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Total Sales");
            series.ChartType = SeriesChartType.Column;
            cmd = new SqlCommand("SELECT YEAR(sdate) AS Year, SUM(total) AS Total FROM vwDailySale GROUP BY YEAR(sdate) ORDER BY Year;", conn);
            conn.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int year = dr.GetInt32(0); // Assuming Year is the first column
                decimal total = dr.GetDecimal(1); // Assuming Total is the second column
                series.Points.AddXY(year, total);
            }
            dr.Close();


            // Set chart title and axis labels
            chart1.Titles.Add("Total Sales by Year");
            chart1.ChartAreas[0].AxisX.Title = "Year";
            chart1.ChartAreas[0].AxisY.Title = "Total Sales";
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblTotal.Text = loadsale() + "$";
            lblStock.Text = loadStock() + " On handed";
            lblTodaySales.Text = dailysale() + "$";
            LoadChart();
        }
    }
}
