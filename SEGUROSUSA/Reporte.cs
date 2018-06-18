using Microsoft.Reporting.WinForms;
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
        public String _sumaPesosTarjeta,_sumaDolaresTarjeta,_sumaPesosEfectivo,_sumaDolaresEfectivo;
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
            SumasValores();
            //SqlCommand sumaDolaresTarjeta = new SqlCommand("SElECT SUM(CANTIDAD) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromadate AND @todate) AND USUARIO=@usuario AND TIPO_DE_PAGO='Dolares' AND FORMA_DE_PAGO='Tarjeta' AND ESTADO='Confirmado';");
            this.VENTATableAdapter.Fill(this.DatosVentas.VENTA, dtpFromDate.Value, dtpToDate.Value, Login._nombreEmpleado);
            ReportParameter p1 = new ReportParameter("Usuario", Login._nombreEmpleado);
            ReportParameter p2 = new ReportParameter("SumaPesosTarjeta", _sumaPesosTarjeta);
            ReportParameter p3 = new ReportParameter("SumaDolaresTarjeta", _sumaDolaresTarjeta);
            ReportParameter p4 = new ReportParameter("SumaPesosEfectivo", _sumaPesosEfectivo);
            ReportParameter p5 = new ReportParameter("SumaDolaresEfectivo", _sumaDolaresEfectivo);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
            this.reportViewer1.RefreshReport();
        }

        private void SumasValores()
        {
            SqlCommand sumaPesosTarjeta = new SqlCommand("SElECT SUM(CANTIDAD_PESOS) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromdate AND @todate) AND USUARIO=@usuario AND FORMA_DE_PAGO='Tarjeta' AND ESTADO='Confirmado';", Connection.ObtenerConexion());
            sumaPesosTarjeta.Parameters.Add(new SqlParameter("fromdate", dtpFromDate.Value));
            sumaPesosTarjeta.Parameters.Add(new SqlParameter("todate", dtpToDate.Value));
            sumaPesosTarjeta.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            SqlCommand sumaPesosEfectivo = new SqlCommand("SElECT SUM(CANTIDAD_PESOS) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromdate AND @todate) AND USUARIO=@usuario AND FORMA_DE_PAGO='Efectivo' AND ESTADO='Confirmado';", Connection.ObtenerConexion());
            sumaPesosEfectivo.Parameters.Add(new SqlParameter("fromdate", dtpFromDate.Value));
            sumaPesosEfectivo.Parameters.Add(new SqlParameter("todate", dtpToDate.Value));
            sumaPesosEfectivo.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            SqlCommand sumaDolaresTarjeta = new SqlCommand("SElECT SUM(CANTIDAD) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromdate AND @todate) AND USUARIO=@usuario AND TIPO_DE_PAGO='Dolares' AND FORMA_DE_PAGO='Tarjeta' AND ESTADO='Confirmado';", Connection.ObtenerConexion());
            sumaDolaresTarjeta.Parameters.Add(new SqlParameter("fromdate", dtpFromDate.Value));
            sumaDolaresTarjeta.Parameters.Add(new SqlParameter("todate", dtpToDate.Value));
            sumaDolaresTarjeta.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            SqlCommand sumaDolaresEfectivo = new SqlCommand("SElECT SUM(CANTIDAD) FROM VENTA WHERE (FECHA_HORA BETWEEN @fromdate AND @todate) AND USUARIO=@usuario AND TIPO_DE_PAGO='Dolares' AND FORMA_DE_PAGO='Efectivo' AND ESTADO='Confirmado';", Connection.ObtenerConexion());
            sumaDolaresEfectivo.Parameters.Add(new SqlParameter("fromdate", dtpFromDate.Value));
            sumaDolaresEfectivo.Parameters.Add(new SqlParameter("todate", dtpToDate.Value));
            sumaDolaresEfectivo.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            try
            {
                _sumaPesosTarjeta = Convert.ToString(sumaPesosTarjeta.ExecuteScalar());
                _sumaPesosEfectivo = Convert.ToString(sumaPesosEfectivo.ExecuteScalar());
                _sumaDolaresTarjeta = Convert.ToString(sumaDolaresTarjeta.ExecuteScalar());
                _sumaDolaresEfectivo = Convert.ToString(sumaDolaresEfectivo.ExecuteScalar());
                if (_sumaPesosTarjeta == "")
                {
                    _sumaPesosTarjeta = "0.00";
                }
                if (_sumaPesosEfectivo == "")
                {
                    _sumaPesosEfectivo = "0.00";
                }
                if (_sumaDolaresTarjeta == "")
                {
                    _sumaDolaresTarjeta = "0.00";
                }
                if (_sumaDolaresEfectivo == "")
                {
                    _sumaDolaresEfectivo = "0.00";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Connection.conn.Close();
            }
        }
    }
}
