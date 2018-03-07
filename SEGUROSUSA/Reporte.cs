using Microsoft.Reporting.WebForms;
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

namespace SEGUROSUSA
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DatosVentas.VENTA' Puede moverla o quitarla según sea necesario.
            //SqlCommand sumaPesosTarjeta = new SqlCommand("SElECT SUM(CANTIDAD_PESOS) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromadate AND @todate) AND USUARIO=@usuario AND FORMA_DE_PAGO='Tarjeta';");
            //SqlCommand sumaDolaresTarjeta = new SqlCommand("SElECT SUM(CANTIDAD) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromadate AND @todate) AND USUARIO=@usuario AND TIPO_DE_PAGO='Dolares' AND FORMA_DE_PAGO='Tarjeta';");
            this.VENTATableAdapter.Fill(this.DatosVentas.VENTA, dtpFromDate.Value, dtpToDate.Value, Login._nombreEmpleado);
            ReportParameter p5 = new ReportParameter("Usuario", Login._nombreEmpleado);
            this.reportViewer1.RefreshReport();
        }
    }
}
