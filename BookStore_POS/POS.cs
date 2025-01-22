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
    public partial class POS : Form
    {
        String ids;
        String Price;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string sittle = "simple pos system";
        SqlDataReader reader;
        SqlDataAdapter da;
        public POS()
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transNo;
                long count;
                conn.Open();
                cmd = new SqlCommand("select top 1 transNo from cart where transNo like '" + sdate + "%' order by id desc", conn);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    transNo = reader[0].ToString();
                    count = Convert.ToInt64(transNo.Substring(8, 4));
                    lblTransNo.Text = sdate + (count + 1);
                    reader.Close();

                }
                else
                {
                    transNo = sdate + "1001";
                    lblTransNo.Text = transNo;
                    reader.Close();
                }


                conn.Close();



            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, sittle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void getTotal()
        {
            double sub = double.Parse(lblTotal.Text);
            double discountAdd = double.Parse(lblDiscount.Text.Replace('%', ' ')) / 100;
            double discount = double.Parse(lblTotal.Text) * discountAdd;
            double Total = double.Parse(lblTotal.Text) - discount;
            double Tax = Total * 0.14;
            double grandtotal = Total + Tax;
            lblTax.Text = Tax.ToString("#,##0.00");
            lblTotalAmount.Text = grandtotal.ToString("#,##0.00") + "$";

        }
        public void loadProduct()
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter("select pcode,barcode,pdes,price,qty from Product_tbl", conn);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add(); // Add a new row
                dataGridView1.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                dataGridView1.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                dataGridView1.Rows[rowIndex].Cells[2].Value = row[2];
                dataGridView1.Rows[rowIndex].Cells[3].Value = row[3];
                dataGridView1.Rows[rowIndex].Cells[4].Value = row[4];
            }
            conn.Close();
        }
        public void loadCart()
        {
            try
            {
                double Total = 0, discount = 0;
                dgvCart.Rows.Clear();
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select c.transNo,p.pdes,p.pcode,c.price,c.qty,c.disc,c.total,c.id from Product_tbl as p inner join cart as c on p.pcode = c.pcode where transNo = '" + lblTransNo.Text + "' and status = 'pending'", conn))
                {
                    // Assuming lbltrans is a TextBox or similar

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Total += double.Parse(reader[6].ToString());
                            discount += double.Parse(reader[5].ToString());
                            dgvCart.Rows.Add(
                                                    reader[0].ToString(),
                                                    reader[1].ToString(),
                                                    reader[2].ToString(),
                                                    reader[3].ToString(),
                                                    reader[4].ToString(),
                                                    reader[5].ToString(),
                                                    reader[6].ToString(),
                                                    reader[7].ToString()


                                                );

                        }
                        conn.Close();
                    }


                    lblTotal.Text = Total.ToString();
                    lblDiscount.Text = discount.ToString();
                    getTotal();
                }


            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "select")
            {
                formqty frm = new formqty(this);
                frm.product_detail(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), double.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()), lblTransNo.Text);
                frm.ShowDialog();
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetTransNo();
            txtsearchbarcode.Enabled = true;
            txtsearchbarcode.Focus();
            dataGridView1.Rows.Clear();
            loadProduct();
        }

        private void POS_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Discount frm = new Discount(this);
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("delete from cart where transNo ='" + lblTransNo.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            loadCart();
            lblTransNo.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }



        private void txtsearchProduct_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter("select pcode,barcode,pdes,price,qty from Product_tbl where pdes like '" + txtsearchProduct.Text + "'", conn);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dataGridView1.Rows.Add(); // Add a new row
                dataGridView1.Rows[rowIndex].Cells[0].Value = row[0]; // Column 1
                dataGridView1.Rows[rowIndex].Cells[1].Value = row[1]; // Column 2
                dataGridView1.Rows[rowIndex].Cells[2].Value = row[2];
                dataGridView1.Rows[rowIndex].Cells[3].Value = row[3];
                dataGridView1.Rows[rowIndex].Cells[4].Value = row[4];
            }
            conn.Close();
        }

        private void txtsearchbarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearchbarcode.Text == string.Empty)
                {
                    return;

                }
                else
                {
                    conn.Open();
                    cmd = new SqlCommand("select *from Product_tbl where barcode like '" + txtsearchbarcode.Text + "'", conn);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        formqty frm = new formqty(this);
                        frm.product_detail(reader["pcode"].ToString(), double.Parse(reader["price"].ToString()), lblTransNo.Text);
                        reader.Close();
                        conn.Close();
                        frm.ShowDialog();
                    }
                    reader.Close();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, sittle);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrder frm = new ViewOrder();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DailySales frm = new DailySales();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Payment frm = new Payment(this);
            frm.ShowDialog();

        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dgvCart.Columns[e.ColumnIndex].Name;
            if (colname == "delete")
            {
            if(MessageBox.Show("Do you want to remove this from your cart ?",sittle,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from cart where id ='" + dgvCart.Rows[e.RowIndex].Cells[7].Value.ToString()+"'",conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadCart();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CancelSale frm = new CancelSale();
            frm.ShowDialog();
        }
    }
    
}

