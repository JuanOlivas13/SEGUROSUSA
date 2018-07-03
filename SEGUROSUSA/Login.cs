using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SEGUROSUSA.Properties;
namespace SEGUROSUSA
{
    public partial class Login : Form
    {
        public static String _nombreEmpleado;
        public static Int32 _idEmpleado;
        public static Int32 _isAdmin;
        public Login()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckbRecordar.Checked)
                {
                    Settings.Default["Usuario"] = txtUsuario.Text;
                    Settings.Default["Password"] = txtContrasena.Text;
                    Settings.Default["Checked"] = ckbRecordar.Checked;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default["Usuario"] = "";
                    Settings.Default["Password"] = "";
                    Settings.Default["Checked"] = ckbRecordar.Checked;
                    Settings.Default.Save();
                }
                SqlCommand searchUser = new SqlCommand(String.Format("Select * From USUARIO Where CUENTA_USUARIO = '{0}' and CONTRASENA = '{1}'", txtUsuario.Text, txtContrasena.Text), Connection.ObtenerConexion());
                SqlDataReader readUser = searchUser.ExecuteReader();
                while (readUser.Read())
                {
                    _nombreEmpleado = readUser.GetString(1);
                    _idEmpleado = readUser.GetInt32(0);
                    _isAdmin = readUser.GetInt32(4);

                }
                if (_nombreEmpleado != null)
                {
                    readUser.Close();
                    Connection.conn.Close();
                    this.Hide();
                }
                else
                {
                    readUser.Close();
                    Connection.conn.Close();
                    MessageBox.Show("Error en los datos.","FALLO AL INICIAR SESION");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"ERROR");
            }
            finally
            {
                Connection.conn.Close();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Environment.Exit(0);
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAceptar_Click(sender, e);
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAceptar_Click(sender, e);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                txtUsuario.Text = (string)Settings.Default["Usuario"];
                txtContrasena.Text = (string)Settings.Default["Password"];
                ckbRecordar.Checked = (bool)Settings.Default["Checked"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(),"ERROR");
            }
        }
    }
}
