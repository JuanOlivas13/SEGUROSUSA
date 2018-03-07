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
    public partial class RegistrarUsuario : Form
    {
        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void _btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ComprobarDatos())
            {
                MessageBox.Show("Ingrese todos los datos");
                return;
            }
            else
            {
                SqlCommand nuevoUsuario = new SqlCommand("INSERT INTO USUARIO (NOMBRE_COMPLETO, CUENTA_USUARIO, CONTRASENA,ADMIN) " +
                    "VALUES(@NOMBRE_COMPLETO, @CUENTA_USUARIO, @CONTRASENA,@ADMIN);", Connection.ObtenerConexion());
                nuevoUsuario.Parameters.Add(new SqlParameter("NOMBRE_COMPLETO", _txtNombre.Text));
                nuevoUsuario.Parameters.Add(new SqlParameter("CUENTA_USUARIO", _txtCuenta.Text));
                nuevoUsuario.Parameters.Add(new SqlParameter("CONTRASENA", _txtContrasena.Text));
                nuevoUsuario.Parameters.Add(new SqlParameter("ADMIN", cmbUsuario.SelectedIndex));
                SqlCommand idNueva = new SqlCommand("SELECT TOP 1 ID_USUARIO FROM USUARIO ORDER BY ID_USUARIO DESC;", Connection.conn);
                try
                {
                    nuevoUsuario.ExecuteNonQuery();
                    MessageBox.Show("Usuario guardado correctamente.");
                    using (SqlDataReader rdr = idNueva.ExecuteReader())
                    {
                        rdr.Read();
                        Usuario nuevo = new Usuario(rdr.GetInt32(0), _txtNombre.Text, _txtCuenta.Text, _txtContrasena.Text,cmbUsuario.SelectedIndex);
                        Usuario.Listausuarios.Add(nuevo);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Connection.conn.Close();
                    this.Close();
                }
            }
        }

        private void _btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarUsuario_Load(object sender, EventArgs e)
        {
            cmbUsuario.SelectedIndex = 0;
        }
        public bool ComprobarDatos()
        {
            if(_txtNombre.Text == "" || _txtCuenta.Text=="" || _txtContrasena.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
