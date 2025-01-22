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
    public partial class Payment : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        POS f;

        public Payment(POS frm)
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            f = frm;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            txtClientMoney.Text += btn.Text;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtClientMoney.Clear();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtClientMoney.Text == null || double.Parse(txtClientMoney.Text) < double.Parse(txtTotalprice.Text.Replace("$"," ")))
            {
                MessageBox.Show("Please enter the Cash!!!", "Payment Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {//KHOS KLAENG NIS
                    int i = 0;
                    for (i = 0; i < f.dgvCart.Rows.Count; i++)
                    {
                        conn.Open();
                        cmd = new SqlCommand("update Product_tbl set qty = qty - " + int.Parse(f.dgvCart.Rows[i].Cells[4].Value.ToString()) + "where pcode = @pcode", conn);
                        cmd.Parameters.AddWithValue("@pcode", f.dgvCart.Rows[i].Cells[2].Value.ToString());
                        cmd.ExecuteNonQuery();

                        conn.Close();
                        conn.Open();
                        cmd = new SqlCommand("update cart set status = 'sold' where transNo = '" + f.lblTransNo.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                    FormReport frm = new FormReport(f);
                    frm.loadReport(txtClientMoney.Text, txtChange.Text);

                    frm.ShowDialog();


                    f.GetTransNo();
                    f.loadProduct();
                    f.dgvCart.Rows.Clear();
                    this.Dispose();






                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtClientMoney_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double totalprice, clientMoney, change;
                totalprice = double.Parse(txtTotalprice.Text.Replace('$',' '));
                clientMoney = double.Parse(txtClientMoney.Text);
                change = clientMoney - totalprice;
                txtChange.Text = change.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            txtTotalprice.Text = f.lblTotalAmount.Text;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
