using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEGUROSUSA
{
    public partial class CardUsuarios : UserControl
    {
        public CardUsuarios()
        {
            InitializeComponent();
        }

        private void CardUsuarios_Load(object sender, EventArgs e)
        {
            _txtNombre.Text = Usuario.auxiliar.nombreCompleto;
            _txtCuenta.Text = Usuario.auxiliar.usuario;
            _txtContrasena.Text = Usuario.auxiliar.contrasena;
            if (Usuario.auxiliar.tipoUsuario == 1)
            {
                _txtTipoUsuario.Text = "Administrador";
            }
            else if (Usuario.auxiliar.tipoUsuario == 0)
            {
                _txtTipoUsuario.Text = "Empleado";
            }
        }

        private void _btnEditar_Click(object sender, EventArgs e)
        {
            EditarUsuario editarUsuario = new EditarUsuario();
            editarUsuario.ShowDialog();
        }
    }
}
