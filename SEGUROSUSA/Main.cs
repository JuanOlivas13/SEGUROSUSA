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
using SEGUROSUSA.Properties;

namespace SEGUROSUSA
{
    public partial class Main : Form
    {
        public static Double _valorDolar, _cantidad, _cantidadpesos, _efectivoDolares, _efectivoPesos, _tarjetaDolares, _tarjetaPesos;
        bool _succes;
        public static String _usuario, _tipodepago, _formadepago, _comentario;
        public static Double _pagoCon, _cambio, _totalPago, _faltanteDolar, _faltantePesos, aux;
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
            lblUsuario2.Text = Login._nombreEmpleado;
            while (_succes == false)
            {
                try
                {
                    _valorDolar = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Ingrese valor del dolar", "Valor del dolar", Settings.Default["ValorDolar"].ToString()));
                    _succes = true;
                    Settings.Default["ValorDolar"] = _valorDolar;
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Favor de ingresar solo numeros. " + ex.Message);
                }
            }
            MostrarVentas();
            MostrarVentasComb();
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
            if (cmbTipodePago.SelectedIndex == 0)
            {
                lblPago.Text = "Dolares";
            }
            else if (cmbTipodePago.SelectedIndex == 1)
            {
                lblPago.Text = "Pesos";
            }
            txtCantidad_Leave(sender, e);
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
            else
            {
                txtCantidad.Text = "0.00";
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
            else
            {
                txtPagoCon.Text = "0.00";
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
            int y = 0;
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
                        dgvVentas.Rows.Add(rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8));
                        if (rdr.GetString(6) == "Cancelada")
                        {
                            dgvVentas.Rows[y].Cells[6].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgvVentas.Rows[y].Cells[6].Style.BackColor = Color.LightGreen;
                        }
                        y++;
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
                MessageBox.Show("Falta ingresar el campo cantidad", "Falta Cantidad");
            }
            else if (txtPagoCon.Text != "")
            {
                _usuario = Login._nombreEmpleado;
                _tipodepago = cmbTipodePago.Text;
                _formadepago = cmbFormadePago.Text;
                _cantidad = Convert.ToDouble(txtCantidad.Text);
                _cantidadpesos = _cantidad * _valorDolar;
                _comentario = "GRACIAS POR SU COMPRA!! \n" + txtComentario.Text;
                if (_cantidad != 0)
                {
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
                    MessageBox.Show("La cantidad esta en 0.00", "Advertencia");
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

        private void ValoresPorDefaultComb()
        {
            txtCantidadComb.Text = "0.00";
            txtDolaresEfectivo.Text = "0.00";
            txtDolaresTarjeta.Text = "0.00";
            txtPesosEfectivo.Text = "0.00";
            txtPesosTarjeta.Text = "0.00";
            txtCantidadComb.Focus();
            lblFaltaCambio.Text = "Falta por Pagar:";
            lblFaltanteDolares.Text = "0.00";
            lblFaltantePesos.Text = "0.00";
            lblPagoTotal.Text = "0.00";
            _totalPago = 0;
            _efectivoDolares = 0;
            _efectivoPesos = 0;
            _tarjetaDolares = 0;
            _tarjetaPesos = 0;
            _cantidad = 0;
            _faltanteDolar = 0;
            _faltantePesos = 0;
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
                    DialogResult respuesta = MessageBox.Show("¿Seguro que desea cancelar la venta?", "Cancelar Venta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

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
            if (dgvVentas.CurrentRow != null)
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
            else
            {
                MessageBox.Show("Ninguna venta seleccionada");
            }
        }

        private void txtCantidadComb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtCantidadComb.Text.IndexOf('.') != -1)
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

        private void txtDolaresEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtDolaresEfectivo.Text.IndexOf('.') != -1)
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

        private void txtPesosEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtPesosEfectivo.Text.IndexOf('.') != -1)
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

        private void txtDolaresEfectivo_Leave(object sender, EventArgs e)
        {
            if (txtDolaresEfectivo.Text != "")
            {
                aux= Convert.ToDouble(txtDolaresEfectivo.Text);
                txtDolaresEfectivo.Text = aux.ToString("F2");
            }
            else
            {
                txtDolaresEfectivo.Text = "0.00";
            }
        }

        private void txtPesosEfectivo_Leave(object sender, EventArgs e)
        {
            if (txtPesosEfectivo.Text != "")
            {
                aux = Convert.ToDouble(txtPesosEfectivo.Text);
                txtPesosEfectivo.Text = aux.ToString("F2");
            }
            else
            {
                txtPesosEfectivo.Text = "0.00";
            }
        }

        private void txtDolaresTarjeta_Leave(object sender, EventArgs e)
        {
            if (txtDolaresTarjeta.Text != "")
            {
                aux = Convert.ToDouble(txtDolaresTarjeta.Text);
                txtDolaresTarjeta.Text = aux.ToString("F2");
            }
            else
            {
                txtDolaresTarjeta.Text = "0.00";
            }
        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            try
            {
                _totalPago = Convert.ToDouble(txtDolaresEfectivo.Text) + Convert.ToDouble(txtDolaresTarjeta.Text);
                _totalPago += ((Convert.ToDouble(txtPesosEfectivo.Text) + Convert.ToDouble(txtPesosTarjeta.Text)) / _valorDolar);
                lblPagoTotal.Text = _totalPago.ToString("F2");
                CalcularFaltante();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular \n" + ex, "ADVERTENCIA");
            }
        }

        private void txtPesosTarjeta_Leave(object sender, EventArgs e)
        {
            if (txtPesosTarjeta.Text != "")
            {
                aux = Convert.ToDouble(txtPesosTarjeta.Text);
                txtPesosTarjeta.Text = aux.ToString("F2");
            }
            else
            {
                txtPesosTarjeta.Text = "0.00";
            }
        }

        private void btnMostrarVentasComb_Click(object sender, EventArgs e)
        {
            MostrarVentasComb();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pestanas.SelectedIndex == 0)
            {
                ValoresPorDefault();
            }
            else if (pestanas.SelectedIndex == 1)
            {
                ValoresPorDefaultComb();
            }
        }

        private void btnCancelarVentaComb_Click(object sender, EventArgs e)
        {
            if (dgvVentasComb.CurrentRow != null)
            {
                var Id = Convert.ToInt32(dgvVentasComb.CurrentRow.Cells[8].Value);
                if (Id != -1)
                {
                    DialogResult respuesta = MessageBox.Show("¿Seguro que desea cancelar la venta?", "Eliminar Venta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (respuesta == DialogResult.OK)
                    {
                        SqlCommand cancelarVentaComb = new SqlCommand("UPDATE VENTACOMBINADA SET ESTADO = 'Cancelada' WHERE ID_VENTACOMB = " + Id + ";", Connection.conn);
                        try
                        {
                            Connection.conn.Open();
                            cancelarVentaComb.ExecuteNonQuery();
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
                    MostrarVentasComb();
                }
            }
            else
            {
                MessageBox.Show("No hay ventas para cancelar");
            }
        }

        private void baseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login._isAdmin == 1)
            {
                Respaldo res = new Respaldo();
                res.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a esta función", "NO PERMITIDO");
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login._nombreEmpleado = null;
            Login login = new Login();
            login.ShowDialog();
            this.Show();
            _succes = false;
            this.WindowState = FormWindowState.Maximized;
            lblUsuario.Text = Login._nombreEmpleado;
            lblUsuario2.Text = Login._nombreEmpleado;
            while (_succes == false)
            {
                try
                {
                    _valorDolar = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Ingrese valor del dolar", "Valor del dolar", "18.5"));
                    _succes = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Favor de ingresar solo numeros. " + ex.Message);
                }
            }
            MostrarVentas();
            MostrarVentasComb();
        }

        private void txtCantidadComb_Leave(object sender, EventArgs e)
        {
            if (txtCantidadComb.Text != "")
            {
                _cantidad = Convert.ToDouble(txtCantidadComb.Text);
                txtCantidadComb.Text = _cantidad.ToString("F2");
                CalcularFaltante();
            }
            else
            {
                txtCantidadComb.Text = "0.00";
            }
        }

        private void CalcularFaltante()
        {
            try
            {
                _faltanteDolar = _cantidad - _totalPago;
                _faltantePesos = _faltanteDolar * _valorDolar;
                if (_faltanteDolar <= 0.0099999)
                {
                    lblFaltaCambio.Text = "El cambio es:";
                    _faltanteDolar *= -1;
                    _faltantePesos *= -1;
                }
                else
                {
                    lblFaltaCambio.Text = "Falta por Pagar:";
                }
                lblFaltanteDolares.Text = _faltanteDolar.ToString("F2");
                lblFaltantePesos.Text = _faltantePesos.ToString("F2");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al calcular \n"+ ex, "ERROR");
            }
        }

        private void txtDolaresTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtDolaresTarjeta.Text.IndexOf('.') != -1)
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

        private void txtPesosTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (e.KeyChar == 46 && txtPesosTarjeta.Text.IndexOf('.') != -1)
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

        private void btnVentaComb_Click(object sender, EventArgs e)
        {
            if (txtCantidadComb.Text == "")
            {
                MessageBox.Show("Falta ingresar el campo cantidad", "Falta Cantidad");
            }
            else if (Convert.ToDouble(lblPagoTotal.Text) >= Convert.ToDouble(txtCantidadComb.Text))
            {
                _usuario = Login._nombreEmpleado;
                _cantidad = Convert.ToDouble(txtCantidadComb.Text);
                _efectivoDolares = Convert.ToDouble(txtDolaresEfectivo.Text);
                _efectivoPesos = Convert.ToDouble(txtPesosEfectivo.Text);
                _tarjetaDolares = Convert.ToDouble(txtDolaresTarjeta.Text);
                _tarjetaPesos = Convert.ToDouble(txtPesosTarjeta.Text);
                _comentario = "GRACIAS POR SU COMPRA!! \n" + txtComentarioComb.Text;
                _totalPago = Convert.ToDouble(lblPagoTotal.Text);
                if (_cantidad != 0)
                {
                    try
                    {
                        SqlCommand ventaComb = VentaCombinada();
                        ventaComb.ExecuteNonQuery();
                        MessageBox.Show(@"Venta hecha correctamente.");
                        TicketComb tkc = new TicketComb();
                        tkc.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        Connection.conn.Close();
                        MostrarVentasComb();
                        ValoresPorDefaultComb();
                    }
                }
                else
                {
                    MessageBox.Show("La cantidad esta en 0.00", "Advertencia");
                    ValoresPorDefaultComb();
                }
            }
            else
            {
                MessageBox.Show("El pago es menor al total", "Advertencia");
            }

        }

        private SqlCommand VentaCombinada()
        {
            SqlCommand ventaComb = new SqlCommand("INSERT INTO VENTACOMBINADA (USUARIO, CANTIDADCOMB, EFECTIVO_DOLARES,EFECTIVO_PESOS,TARJETA_DOLARES,TARJETA_PESOS,FECHA_HORA,VALOR_DOLAR,PAGO,ESTADO) VALUES(@USUARIO, @CANTIDADCOMB, @EFECTIVO_DOLARES,@EFECTIVO_PESOS,@TARJETA_DOLARES,@TARJETA_PESOS,@FECHA_HORA,@VALOR_DOLAR,@PAGO,@ESTADO);", Connection.ObtenerConexion());
            ventaComb.Parameters.Add(new SqlParameter("USUARIO", Login._nombreEmpleado));
            ventaComb.Parameters.Add(new SqlParameter("CANTIDADCOMB", Convert.ToDouble(txtCantidadComb.Text)));
            ventaComb.Parameters.Add(new SqlParameter("EFECTIVO_DOLARES", Convert.ToDouble(txtDolaresEfectivo.Text)));
            ventaComb.Parameters.Add(new SqlParameter("EFECTIVO_PESOS", Convert.ToDouble(txtPesosEfectivo.Text)));
            ventaComb.Parameters.Add(new SqlParameter("TARJETA_DOLARES", Convert.ToDouble(txtDolaresTarjeta.Text)));
            ventaComb.Parameters.Add(new SqlParameter("TARJETA_PESOS", Convert.ToDouble(txtPesosTarjeta.Text)));
            ventaComb.Parameters.Add(new SqlParameter("FECHA_HORA", DateTime.Now));
            ventaComb.Parameters.Add(new SqlParameter("VALOR_DOLAR", _valorDolar));
            ventaComb.Parameters.Add(new SqlParameter("PAGO", Convert.ToDouble(lblPagoTotal.Text)));
            ventaComb.Parameters.Add(new SqlParameter("ESTADO", "Confirmado"));
            return ventaComb;
        }

        private void btnReimpresionComb_Click(object sender, EventArgs e)
        {
            if (dgvVentasComb.CurrentRow != null)
            {
                _usuario = Login._nombreEmpleado;
                _cantidad = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[1].Value);
                _efectivoDolares = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[2].Value);
                _efectivoPesos = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[3].Value);
                _tarjetaDolares = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[4].Value);
                _tarjetaPesos = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[5].Value);
                _comentario = "GRACIAS POR SU COMPRA!! \n";
                _totalPago = Convert.ToDouble(dgvVentasComb.CurrentRow.Cells[9].Value);
                TicketComb tkc = new TicketComb();
                tkc.ShowDialog();
                ValoresPorDefaultComb();
            }
            else
            {
                MessageBox.Show("Ninguna venta seleccionada");
            }
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
                MessageBox.Show("No tiene permisos para acceder a esta función", "NO PERMITIDO");
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
                    _valorDolar = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox("Ingrese valor del dolar", "Valor del dolar", Settings.Default["ValorDolar"].ToString()));
                    _succes = true;
                    Settings.Default["ValorDolar"] = _valorDolar;
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Favor de ingresar solo numeros. " + ex.Message);
                }
            }
        }

        public void MostrarVentasComb()
        {
            int x = 0;
            dgvVentasComb.Rows.Clear();
            dgvVentasComb.AutoGenerateColumns = false;
            SqlCommand selectVentasComb = new SqlCommand("SELECT USUARIO,CANTIDADCOMB,EFECTIVO_DOLARES,EFECTIVO_PESOS,TARJETA_DOLARES,TARJETA_PESOS,FECHA_HORA,ESTADO,ID_VENTACOMB,PAGO from VENTACOMBINADA where FECHA_HORA >= @fecha AND USUARIO=@usuario;", Connection.ObtenerConexion());
            selectVentasComb.Parameters.Add(new SqlParameter("usuario", Login._nombreEmpleado));
            selectVentasComb.Parameters.Add(new SqlParameter("fecha", dtpFechaComb.Value.Date));
            try
            {
                using (SqlDataReader rdr = selectVentasComb.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        dgvVentasComb.Rows.Add(rdr.GetValue(0), rdr.GetValue(1), rdr.GetValue(2), rdr.GetValue(3), rdr.GetValue(4), rdr.GetValue(5), rdr.GetValue(6), rdr.GetValue(7), rdr.GetValue(8), rdr.GetValue(9));
                        if (rdr.GetString(7) == "Cancelada")
                        {
                            dgvVentasComb.Rows[x].Cells[7].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgvVentasComb.Rows[x].Cells[7].Style.BackColor = Color.LightGreen;
                        }
                        x++;
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

    }
}
