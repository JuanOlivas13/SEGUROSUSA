using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SEGUROSUSA
{
    public partial class TicketComb : Form
    {
        public TicketComb()
        {
            InitializeComponent();
        }

        private void TicketConv_Load(object sender, EventArgs e)
        {
            try
            {
                ReportParameter p1 = new ReportParameter("CantidadComb", Main._cantidad.ToString("0.00"));
                ReportParameter p2 = new ReportParameter("DolaresEfectivo", Main._efectivoDolares.ToString("0.00"));
                ReportParameter p3 = new ReportParameter("DolaresTarjeta", Main._tarjetaDolares.ToString("0.00"));
                ReportParameter p4 = new ReportParameter("PesosTarjeta", Main._tarjetaPesos.ToString("0.00"));
                ReportParameter p5 = new ReportParameter("PesosEfectivo", Main._efectivoPesos.ToString("0.00"));
                ReportParameter p6 = new ReportParameter("Usuario", Main._usuario);
                ReportParameter p7 = new ReportParameter("Comentarios", Main._comentario);
                ReportParameter p8 = new ReportParameter("Cambio", Main._faltanteDolar.ToString("0.00"));
                ReportParameter p9 = new ReportParameter("Pago", Main._totalPago.ToString("0.00"));
                this.reportViewerConv.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 });
                this.reportViewerConv.RefreshReport();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al generar ticket \n" + ex);
            }
        }
    }
}
