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
    public partial class formqty : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string sittle = "simple pos system";
        private string pcode;
        private double price;
        private String transNo;
        SqlDataReader reader;
        POS pos;
        public formqty(POS frmpos)
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            pos = frmpos;
        }
        private void formqty_Load(object sender, EventArgs e)
        {

        }
        public void product_detail(String pcode, double price, String transNo)
        {
            this.pcode = pcode;
            this.price = price;
            this.transNo = transNo;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && (textBox1.Text != String.Empty))
            {
                conn.Open();
                cmd = new SqlCommand("insert into cart (transNo,pcode,price,qty,sdate) values(@transNo,@pid,@price,@qty,@sdate)", conn);
                cmd.Parameters.AddWithValue("@transNo", transNo);
                cmd.Parameters.AddWithValue("@pid", pcode);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@qty", textBox1.Text);
                cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                conn.Close();
                pos.txtsearchbarcode.Clear();
                pos.loadCart();
                pos.loadProduct();
                this.Close();

            }

        }
    }
}
