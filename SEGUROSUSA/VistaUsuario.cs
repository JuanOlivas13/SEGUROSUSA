using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SEGUROSUSA
{
    public partial class VistaUsuario : UserControl
    {
        public VistaUsuario()
        {
            InitializeComponent();
        }

        private void VistaUsuario_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            _txtBuscar.Focus();
            _lstbConsulta.BeginUpdate();
            foreach (Usuario row in Usuario.Listausuarios)
            {
                _lstbConsulta.Items.Add(row);
            }
            _lstbConsulta.EndUpdate();
        }

        private void _txtBuscar_TextChanged(object sender, EventArgs e)
        {
            _lstbConsulta.BeginUpdate();
            _lstbConsulta.Items.Clear();
            foreach (Usuario row in Usuario.Listausuarios)
            {
                if (row.nombreCompleto.ToLower().Contains(_txtBuscar.Text))
                {
                    _lstbConsulta.Items.Add(row);
                }
            }
            _lstbConsulta.EndUpdate();
        }

        public void listen(string evento)
        {
            _lstbConsulta.BeginUpdate();
            _lstbConsulta.Items.Clear();
            foreach (Usuario row in Usuario.Listausuarios)
            {
                _lstbConsulta.Items.Add(row);
            }
            _lstbConsulta.SelectedItem = Usuario.auxiliar;
            _lstbConsulta.EndUpdate();
        }

        private void _lstbConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lstbConsulta.SelectedItem != null)
            {
                Usuario.auxiliar = (Usuario)_lstbConsulta.SelectedItem;
                navigator1.NavigateTo(new CardUsuarios());
            }
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            if (_lstbConsulta.SelectedIndex != -1)
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar a " + _lstbConsulta.SelectedItem.ToString() + "?", "Eliminar Usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.OK)
                {
                    Usuario  usuarioBorrado = (Usuario)_lstbConsulta.SelectedItem;
                    SqlCommand eliminarUsuario = new SqlCommand("DELETE FROM USUARIO WHERE ID_USUARIO = " + usuarioBorrado.idUsuario + ";", Connection.ObtenerConexion());

                    try
                    { 
                        eliminarUsuario.ExecuteNonQuery();
                        MessageBox.Show("Usuario eliminado correctamente.");
                        Usuario.Listausuarios.Remove(usuarioBorrado);
                        _lstbConsulta.BeginUpdate();
                        _lstbConsulta.Items.Clear();
                        foreach (Usuario row in Usuario.Listausuarios)
                        {
                            _lstbConsulta.Items.Add(row);
                        }
                        _lstbConsulta.EndUpdate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        Connection.conn.Close();
                        navigator1.ClearNavigator();
                    }
                }
            }
        }

        private void _btnRegistrar_Click(object sender, EventArgs e)
        {
            var registrarUsuario = new RegistrarUsuario();
            registrarUsuario.ShowDialog();
            _lstbConsulta.BeginUpdate();
            _lstbConsulta.Items.Clear();
            foreach (Usuario row in Usuario.Listausuarios)
            {
                _lstbConsulta.Items.Add(row);
            }
            _lstbConsulta.EndUpdate();
            navigator1.ClearNavigator();
        }
    }
}
