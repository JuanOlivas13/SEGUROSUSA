using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEGUROSUSA
{
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            ReportParameter p1 = new ReportParameter("Cantidad", Main._cantidad.ToString("0.00"));
            ReportParameter p2 = new ReportParameter("Tipodepago", Main._tipodepago);
            ReportParameter p3 = new ReportParameter("Usuario", Main._usuario);
            ReportParameter p4 = new ReportParameter("FormadePago", Main._formadepago);
            ReportParameter p6 = new ReportParameter("Comentarios", Main._comentario);
            ReportParameter p7 = new ReportParameter("Cambio", Main._cambio.ToString("0.00"));
            ReportParameter p8 = new ReportParameter("PagoCon", Main._pagoCon.ToString("0.00"));
            ReportParameter p5;
            if (Main._tipodepago=="Dolares")
            {
                p5 = new ReportParameter("Cantidadpesos",Main._cantidad.ToString("0.00") + " Dolares");
            }
            else
            {
                p5 = new ReportParameter("Cantidadpesos",Main._cantidadpesos.ToString("0.00") + " Pesos");
            }
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8 });
            this.reportViewer1.RefreshReport();
        }
    }
}
