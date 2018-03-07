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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();

            Usuario.Listausuarios = new List<Usuario>();
            SqlCommand selectUsuarios = new SqlCommand("SELECT * FROM USUARIO;", Connection.ObtenerConexion());
            navigator1.NavigateTo(new VistaUsuario());
            try
            {

                using (SqlDataReader rdr = selectUsuarios.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Usuario nuevoUsuario = new Usuario(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetString(3),
                            rdr.GetInt32(4)
                        );
                        Usuario.Listausuarios.Add(nuevoUsuario);
                    }
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
