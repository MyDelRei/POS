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
    public partial class Stock : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Stock()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void loadStockInhistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT *from vwStock where cast(sdate as date) between '" + dtp1.Value.ToShortDateString() + "'and'" + dtp2.Value.ToShortDateString() + "' and status like 'Done'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());

            }
            reader.Close();


            conn.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (MessageBox.Show("Do you want to save this to stock?", "Title", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        // Prepare the quantity to update
                        int quantityToUpdate = int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());
                        string pcode = dataGridView2.Rows[i].Cells[3].Value.ToString();
                        string stockId = dataGridView2.Rows[i].Cells[1].Value.ToString();

                        // Update qty product
                        using (SqlCommand cmd = new SqlCommand("UPDATE Product_Tbl SET qty = qty + @Quantity WHERE pcode = @Pcode", conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantityToUpdate);
                            cmd.Parameters.AddWithValue("@Pcode", pcode);
                            cmd.ExecuteNonQuery();
                        }

                        // Update stock qty
                        using (SqlCommand cmd = new SqlCommand("UPDATE stock SET qty = qty + @Quantity, status = 'Done' WHERE id = @Id", conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantityToUpdate);
                            cmd.Parameters.AddWithValue("@Id", stockId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                    loadstock();
                }

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colName == "delete")
            {
                if (MessageBox.Show("do you want to delete this ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("Delete from stock where id like '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("delete successful");
                    loadstock();
                }
            }
        }


        public void loadstock()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("SELECT *from vwStock where refNo like '" + txtref.Text + "'and status like 'Pending'", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());

            }
            reader.Close();


            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            searchProduct frm = new searchProduct(this);
            frm.loadProduct();
            frm.ShowDialog();
        }

        private void load_Click(object sender, EventArgs e)
        {
            loadStockInhistory();
        }

    }
}
