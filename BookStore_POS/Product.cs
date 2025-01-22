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
    public partial class Product : Form
    {
        SqlDataAdapter da;
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt;
        public Product()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            load_Product();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddProduct frm = new AddProduct(this);
            frm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void load_Product()
        {
            try
            {
                conn.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("select pcode,barcode,pdes,price,qty from Product_tbl", conn);
                da.Fill(dt);
                conn.Close();
                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add(); // Add a new row
                    dataGridView1.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                    dataGridView1.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                    dataGridView1.Rows[rowIndex].Cells[2].Value = row[2];
                    dataGridView1.Rows[rowIndex].Cells[3].Value = row[3] + "$";
                    dataGridView1.Rows[rowIndex].Cells[4].Value = row[4];
                }
            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "edit")
            {
                AddProduct frm = new AddProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.txtProductCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                frm.txtBarcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtqty.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.ShowDialog();
                load_Product();

            }
            else if (colname == "delete")
            {
                if (MessageBox.Show("Do you want to delete this form ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from Product_tbl where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    load_Product();


                }
            }
        }
    }
}
