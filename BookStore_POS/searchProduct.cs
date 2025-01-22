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
    public partial class searchProduct : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string sittle = "simple pos system";
        SqlDataReader reader;
        Stock frm;
        public searchProduct(Stock flist)
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            frm = flist;
        }
        public void loadProduct()
        {
            int i = 0;

            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("select pcode,pdes,qty from Product_Tbl where pdes like '%" + txtSearch.Text + "%' order by pdes", conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            conn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName == "Add")
            {
                if (frm.txtref.Text == string.Empty) { MessageBox.Show("please enter the reference number", sittle, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (frm.txtstockinby.Text == string.Empty) { MessageBox.Show("please enter the supplier name ", sittle, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    if (MessageBox.Show("do you want to add this to stock", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        conn.Open();
                        cmd = new SqlCommand("insert into stock (refNo,pcode,sdate,stockinby) values(@refNo,@pcode,@sdate,@stockinby)", conn);
                        cmd.Parameters.AddWithValue("@refNo", frm.txtref.Text);
                        cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@sdate", frm.dtpstockindate.Value);
                        cmd.Parameters.AddWithValue("@stockinby", frm.txtstockinby.Text);

                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Save successful", sittle, MessageBoxButtons.OK);
                        frm.loadstock();
                    }
                }
            }
        }
    }
}
