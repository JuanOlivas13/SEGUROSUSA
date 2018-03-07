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
    public partial class EditarUsuario : Form
    {
        public EditarUsuario()
        {
            InitializeComponent();
        }

        private void EditarUsuario_Load(object sender, EventArgs e)
        {
            _txtNombre.Text = Usuario.auxiliar.nombreCompleto;
            _txtCuenta.Text = Usuario.auxiliar.usuario;
            _txtContrasena.Text = Usuario.auxiliar.contrasena;
            cmbUsuario.SelectedIndex = Usuario.auxiliar.tipoUsuario;
        }

        private void _btnEditar_Click(object sender, EventArgs e)
        {
            SqlCommand editarUsuario = new SqlCommand("UPDATE USUARIO SET NOMBRE_COMPLETO = @NOMBRE_COMPLETO, CUENTA_USUARIO = @CUENTA_USUARIO, CONTRASENA = @CONTRASENA, ADMIN=@ADMIN WHERE ID_USUARIO = @ID_USUARIO;", Connection.ObtenerConexion());
            editarUsuario.Parameters.Add(new SqlParameter("NOMBRE_COMPLETO", _txtNombre.Text));
            editarUsuario.Parameters.Add(new SqlParameter("CUENTA_USUARIO", _txtCuenta.Text));
            editarUsuario.Parameters.Add(new SqlParameter("CONTRASENA", _txtContrasena.Text));
            editarUsuario.Parameters.Add(new SqlParameter("ADMIN", cmbUsuario.SelectedIndex));
            editarUsuario.Parameters.Add(new SqlParameter("ID_USUARIO", Usuario.auxiliar.idUsuario));

            Usuario.auxiliar.nombreCompleto = _txtNombre.Text;
            Usuario.auxiliar.usuario = _txtCuenta.Text;
            Usuario.auxiliar.contrasena = _txtContrasena.Text;
            Usuario.auxiliar.tipoUsuario = cmbUsuario.SelectedIndex;

            try
            {
                editarUsuario.ExecuteNonQuery();
                MessageBox.Show("Usuario editado correctamente");
                int i = 0;
                foreach (Usuario row in Usuario.Listausuarios)
                {
                    if (row.idUsuario == Usuario.auxiliar.idUsuario)
                    {
                        Usuario.Listausuarios[i] = Usuario.auxiliar;
                        break;
                    }
                    i += 1;
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

        private void _btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
