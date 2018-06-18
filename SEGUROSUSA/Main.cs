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
    public partial class Main : Form
    {
        public static Double _valorDolar, _cantidad,_cantidadpesos;
        bool _succes;
        public static String _usuario, _tipodepago, _formadepago,_comentario;
        public static Double _pagoCon, _cambio;
        public Main()
        {
            InitializeComponent();
            cmbFormadePago.SelectedIndex = 0;
            cmbTipodePago.SelectedIndex = 0;
            _succes = false;
            dtpVentas.Value = DateTime.Now;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.WindowState = FormWindowState.Maximized;
            lblUsuario.Text = Login._nombreEmpleado;
            while (_succes == false)
            {
                try
                {
                    _valorDolar = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Ingrese valor del dolar", "Valor del dolar", "18"));
                    _succes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Favor de ingresar solo numeros. " + ex.Message);
                }
            }
            MostrarVentas();
        }

        private void cmbFormadePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormadePago.SelectedIndex == 0)
            {
                txtPagoCon.ReadOnly = false;
                txtPagoCon.Text = "0.00";
            }
            else
            {
                txtPagoCon.Clear();
                txtPagoCon.ReadOnly = true;
                txtPagoCon.Text = "0.00";
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtCantidad.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtCantidad.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbTipodePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipodePago.SelectedIndex==0)
            {
                lblPago.Text = "Dolares";
            }
            else if (cmbTipodePago.SelectedIndex == 1)
            {
                lblPago.Text = "Pesos";
            }
            txtCantidad_Leave(sender,e);
            txtPagoCon_Leave(sender, e);
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "")
            {
                _cantidad = Convert.ToDouble(txtCantidad.Text);
                if (cmbTipodePago.SelectedIndex == 0)
                {
                    lblTotal.Text = _cantidad.ToString("F2");
                    txtCantidad.Text = lblTotal.Text;
                }
                else
                {
                    lblTotal.Text = (Convert.ToDouble(txtCantidad.Text) * _valorDolar).ToString("0.00");
                    txtCantidad.Text = _cantidad.ToString("F2");
                }
            }
        }

        private void txtPagoCon_Leave(object sender, EventArgs e)
        {
            if (txtPagoCon.Text != "")
            {
                _pagoCon = Convert.ToDouble(txtPagoCon.Text);
                if (cmbTipodePago.SelectedIndex == 0 && cmbFormadePago.SelectedIndex == 0)
                {
                    _cambio = Convert.ToDouble(txtPagoCon.Text) - Convert.ToDouble(txtCantidad.Text);
                    lblCambio.Text = _cambio.ToString("0.00");
                    txtPagoCon.Text = _pagoCon.ToString("F2");
                }
                else if (cmbTipodePago.SelectedIndex == 1 && cmbFormadePago.SelectedIndex == 0)
                {
                    _cambio = Convert.ToDouble(txtPagoCon.Text) - (Convert.ToDouble(txtCantidad.Text) * _valorDolar);
                    lblCambio.Text = _cambio.ToString("0.00");
                    txtPagoCon.Text = _pagoCon.ToString("F2");
                }
            }
        }

        private void cambiarElValorDelDolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cambiarValorDolar();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            MostrarVentas();
        }

        public void MostrarVentas()
        {
            dgvVentas.Rows.Clear();
            dgvVentas.AutoGenerateColumns = false;
            SqlCommand selectVentas = new SqlCommand("SELECT USUARIO,CANTIDAD,TIPO_DE_PAGO,FECHA_HORA,FORMA_DE_PAGO,CANTIDAD_PESOS,ESTADO,ID_VENTA,PAGO from VENTA where FECHA_HORA >= @fecha AND USUARIO=@usuario;", Connection.ObtenerConexion());
            selectVentas.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            selectVentas.Parameters.Add(new SqlParameter("fecha", dtpVentas.Value.Date));
            try
            {
                using (SqlDataReader rdr = selectVentas.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        dgvVentas.Rows.Add(rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4),rdr.GetValue(5),rdr.GetValue(6), rdr.GetValue(7),rdr.GetValue(8));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.conn.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        { 
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("Falta ingresar el campo cantidad","Falta Cantidad");
            }
            else if (txtPagoCon.Text!="")
            {
                _usuario = Login._nombreEmpleado;
                _tipodepago = cmbTipodePago.Text;
                _formadepago = cmbFormadePago.Text;
                _cantidad = Convert.ToDouble(txtCantidad.Text);
                _cantidadpesos = _cantidad * _valorDolar;
                _comentario = "GRACIAS POR SU COMPRA!! \n" + txtComentario.Text;
                if (cmbTipodePago.SelectedIndex == 0 && cmbFormadePago.SelectedIndex == 0)
                {
                    _pagoCon = Convert.ToDouble(txtPagoCon.Text);
                    if (_pagoCon < _cantidad)
                    {
                        MessageBox.Show("El pago es menor al total");
                        return;
                    }
                    else
                    {
                        _cambio = _pagoCon - _cantidad;
                    }
                    
                }
                else if (cmbTipodePago.SelectedIndex == 1 && cmbFormadePago.SelectedIndex == 0)
                {
                    _pagoCon = Convert.ToDouble(txtPagoCon.Text);
                    if (_pagoCon < _cantidadpesos)
                    {
                        MessageBox.Show("El pago es menor al total");
                        return;
                    }
                    else
                    {
                        _cambio = _pagoCon - _cantidadpesos;
                    }
                }
                else
                {
                    _pagoCon = 0;
                    _cambio = 0;
                }
                if (cmbTipodePago.SelectedIndex == 0)
                {
                    SqlCommand nuevaVenta = VentaDolares();
                    try
                    {
                        nuevaVenta.ExecuteNonQuery();
                        MessageBox.Show(@"Venta hecha correctamente.");
                        Ticket tk = new Ticket();
                        tk.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        Connection.conn.Close();
                        MostrarVentas();
                        ValoresPorDefault();
                    }
                }
                else if (cmbTipodePago.SelectedIndex == 1)
                {
                    SqlCommand nuevaVenta = VentaPesos();
                    try
                    {
                        nuevaVenta.ExecuteNonQuery();
                        MessageBox.Show(@"Venta hecha correctamente.");
                        Ticket tk = new Ticket();
                        tk.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        Connection.conn.Close();
                        MostrarVentas();
                        ValoresPorDefault();
                    }
                }
            }
            else
            {
                MessageBox.Show("Falto ingresar el campo pago", "Falta Pago");
            }
        }

        private void ValoresPorDefault()
        {
            txtCantidad.Text = "0.00";
            txtPagoCon.Text = "0.00";
            txtComentario.Text = "";
            lblTotal.Text = "0.00";
            lblCambio.Text = "0.00";
            txtCantidad.Focus();
            cmbFormadePago.SelectedIndex = 0;
            cmbTipodePago.SelectedIndex = 0;
        }

        private SqlCommand VentaPesos()
        {
            SqlCommand nuevaVenta = new SqlCommand("INSERT INTO VENTA (USUARIO, CANTIDAD, TIPO_DE_PAGO, FECHA_HORA, FORMA_DE_PAGO,VALOR_DOLAR,CANTIDAD_PESOS,PAGO,ESTADO) VALUES(@USUARIO, @CANTIDAD, @TIPO_DE_PAGO, @FECHA_HORA, @FORMA_DE_PAGO,@VALOR_DOLAR,@CANTIDAD_PESOS,@PAGO,@ESTADO);", Connection.ObtenerConexion());
            nuevaVenta.Parameters.Add(new SqlParameter("USUARIO", Login._nombreEmpleado));
            nuevaVenta.Parameters.Add(new SqlParameter("CANTIDAD", Convert.ToDouble(txtCantidad.Text)));
            nuevaVenta.Parameters.Add(new SqlParameter("TIPO_DE_PAGO", cmbTipodePago.Text));
            nuevaVenta.Parameters.Add(new SqlParameter("FECHA_HORA", DateTime.Now));
            nuevaVenta.Parameters.Add(new SqlParameter("FORMA_DE_PAGO", cmbFormadePago.Text));
            nuevaVenta.Parameters.Add(new SqlParameter("VALOR_DOLAR", _valorDolar));
            nuevaVenta.Parameters.Add(new SqlParameter("CANTIDAD_PESOS", (Convert.ToDouble(txtCantidad.Text) * _valorDolar)));
            nuevaVenta.Parameters.Add(new SqlParameter("ESTADO", "Confirmado"));
            nuevaVenta.Parameters.Add(new SqlParameter("PAGO", Convert.ToDouble(txtPagoCon.Text)));
            return nuevaVenta;
        }

        private void corteDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte rp = new Reporte();
            rp.ShowDialog();
        }

        private void lblCambio_TextChanged(object sender, EventArgs e)
        {
            if (_cambio < 0)
            {
                lblCambio.Text = "0.00";
                txtPagoCon.Text = "0.00";
            }
        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbTipodePago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbFormadePago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                var Id = Convert.ToInt32(dgvVentas.CurrentRow.Cells[7].Value);
                if (Id != -1)
                {
                    DialogResult respuesta = MessageBox.Show("¿Seguro que desea cancelar la venta", "Eliminar Venta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.OK)
                    {
                        SqlCommand cancelarVenta = new SqlCommand("UPDATE VENTA SET ESTADO = 'Cancelada' WHERE ID_VENTA = " + Id + ";", Connection.conn);
                        try
                        {
                            Connection.conn.Open();
                            cancelarVenta.ExecuteNonQuery();
                            MessageBox.Show("Venta cancelada correctamente.");
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
                    MostrarVentas();
                }
            }
            else
            {
                MessageBox.Show("No hay ventas para cancelar");
            }
        }

        private void btnReimpresion_Click(object sender, EventArgs e)
        {
            _usuario = Login._nombreEmpleado;
            _cantidad = Convert.ToDouble(dgvVentas.CurrentRow.Cells[1].Value);
            _tipodepago = dgvVentas.CurrentRow.Cells[2].Value.ToString();
            _formadepago = dgvVentas.CurrentRow.Cells[4].Value.ToString();
            _pagoCon = Convert.ToDouble(dgvVentas.CurrentRow.Cells[8].Value);
            _comentario = "GRACIAS POR SU COMPRA!! \n";
            if (dgvVentas.CurrentRow.Cells[5].Value.ToString() != "")
            {
                _cantidadpesos = Convert.ToDouble(dgvVentas.CurrentRow.Cells[5].Value);
            }
            else
            {
                _cantidadpesos = 0;
            }
            if (_formadepago == "Efectivo")
            {
                if (_tipodepago == "Dolares")
                {
                    _cambio = _pagoCon - _cantidad;
                }
                else
                {
                    _cambio = _pagoCon - _cantidadpesos;
                }
            }
            else
            {
                _cambio = 0;
            }
            Ticket tk = new Ticket();
            tk.ShowDialog();
            ValoresPorDefault();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login._isAdmin==1)
            {
                Usuarios us = new Usuarios();
                us.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a esta función", "No permitido");
            }
        }

        private void usuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Login._isAdmin == 1)
            {
                Usuarios us = new Usuarios();
                us.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a esta función", "No permitido");
            }
        }

        private void corteDeCajaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reporte rp = new Reporte();
            rp.ShowDialog();
        }

        private SqlCommand VentaDolares()
        {
            SqlCommand nuevaVenta = new SqlCommand("INSERT INTO VENTA (USUARIO, CANTIDAD, TIPO_DE_PAGO, FECHA_HORA, FORMA_DE_PAGO,VALOR_DOLAR,PAGO,ESTADO) VALUES(@USUARIO, @CANTIDAD, @TIPO_DE_PAGO, @FECHA_HORA, @FORMA_DE_PAGO,@VALOR_DOLAR,@PAGO,@ESTADO);", Connection.ObtenerConexion());
            nuevaVenta.Parameters.Add(new SqlParameter("USUARIO", Login._nombreEmpleado));
            nuevaVenta.Parameters.Add(new SqlParameter("CANTIDAD", Convert.ToDouble(txtCantidad.Text)));
            nuevaVenta.Parameters.Add(new SqlParameter("TIPO_DE_PAGO", cmbTipodePago.Text));
            nuevaVenta.Parameters.Add(new SqlParameter("FECHA_HORA", DateTime.Now));
            nuevaVenta.Parameters.Add(new SqlParameter("FORMA_DE_PAGO", cmbFormadePago.Text));
            nuevaVenta.Parameters.Add(new SqlParameter("VALOR_DOLAR", _valorDolar));
            nuevaVenta.Parameters.Add(new SqlParameter("PAGO", Convert.ToDouble(txtPagoCon.Text)));
            nuevaVenta.Parameters.Add(new SqlParameter("ESTADO", "Confirmado"));
            return nuevaVenta;
        }

        private void modificarValorDelDolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cambiarValorDolar();
        }

        private void cambiarValorDolar()
        {
            _succes = false;
            while (_succes == false)
            {
                try
                {
                    _valorDolar = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Ingrese valor del dolar", "Valor del dolar", "18"));
                    _succes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Favor de ingresar solo numeros. " + ex.Message);
                }
            }
        }
    }
}
