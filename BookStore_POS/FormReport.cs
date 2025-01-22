using Microsoft.Reporting.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BookStore_POS
{
    public partial class FormReport : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da;
        POS f;

        String Store = "Solidity BookStore";
        String Address = "Bekchan Angsnoul Kandal";
        public FormReport(POS frm)
        {
            InitializeComponent();
            conn = new SqlConnection(dbcon.GetConnection());
            f = frm;

        }

        private void reciept_Load(object sender, EventArgs e)
        {

            this.rptView.RefreshReport();
        }
        public void loadReport(string Cash, string change)
        {

            try
            {
                this.rptView.LocalReport.ReportEmbeddedResource = "BookStore_POS.Report1.rdlc";
                this.rptView.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                conn.Open();
                da.SelectCommand = new SqlCommand("Select c.id, c.transNo, c.pcode, c.price, c.qty, c.disc, c.total, c.sdate, c.status, p.pdes from cart as c inner join Product_Tbl as p on p.pcode = c.pcode where transNo like '" + f.lblTransNo.Text + "'", conn);
                da.Fill(ds.Tables["dtSold"]);
                conn.Close();


                ReportParameter pTax = new ReportParameter("pTax", f.lblTax.Text);
                ReportParameter pDiscount = new ReportParameter("pDiscount", f.lblDiscount.Text);
                ReportParameter pTotal = new ReportParameter("pTotal", f.lblTotalAmount.Text);
                ReportParameter pCash = new ReportParameter("pCash", Cash);
                ReportParameter pChange = new ReportParameter("pChange", change);
                ReportParameter pStore = new ReportParameter("pStore", Store);
                ReportParameter pAddress = new ReportParameter("pAddress", Address);
                ReportParameter pTransNo = new ReportParameter("pTransaction", "Invoice #: " + f.lblTransNo.Text);

                
                rptView.LocalReport.SetParameters(pTax);
                rptView.LocalReport.SetParameters(pDiscount);
                rptView.LocalReport.SetParameters(pTotal);
                rptView.LocalReport.SetParameters(pCash);
                rptView.LocalReport.SetParameters(pChange);
                rptView.LocalReport.SetParameters(pStore);
                rptView.LocalReport.SetParameters(pAddress);
                rptView.LocalReport.SetParameters(pTransNo);

                rptView.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables["dtSold"]));
                this.rptView.RefreshReport();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }


        }
    }
}
