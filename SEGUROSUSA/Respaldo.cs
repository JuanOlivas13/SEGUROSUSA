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
using System.IO;

namespace SEGUROSUSA
{
    public partial class Respaldo : Form
    {
        public Respaldo()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlg.SelectedPath+ "\\SEGUROSUSA-" + DateTime.Now.ToString("dd-MM-yyyy") + ".bak"))
                {
                    DialogResult existe = MessageBox.Show("Ya hay un respaldo con ese nombre \n ¿Seguro que desea sobreescribirlo?", "Sobreescribir Respaldo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (existe == DialogResult.OK)
                    {
                        txtRespaldo.Text = dlg.SelectedPath;
                        btnRespaldo.Enabled = true;
                    }
                }
                else
                {
                    txtRespaldo.Text = dlg.SelectedPath;
                    btnRespaldo.Enabled = true;
                }

            }
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRespaldo.Text == String.Empty)
                {
                    MessageBox.Show("Por favor seleccione una ruta para el respaldo");
                }
                else
                {
                    SqlCommand respaldo = new SqlCommand("BACKUP DATABASE SEGUROSUSA TO DISK = '" + txtRespaldo.Text+ "\\SEGUROSUSA-" + DateTime.Now.ToString("dd-MM-yyyy") + ".bak' WITH INIT", Connection.ObtenerConexion());
                    respaldo.ExecuteNonQuery();
                    MessageBox.Show("Respaldo creado con exito");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se completo el respaldo \n" + ex.ToString());
            }
            finally
            {
                Connection.conn.Close();
                btnRespaldo.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL SERVER  database backup files|*.bak";
            ofd.Title = "Seleccione base de datos para restaurar";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtRastaurar.Text = ofd.FileName;
                btnRestaurar.Enabled = true;
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand restaurar1 = new SqlCommand("ALTER DATABASE SEGUROSUSA SET SINGLE_USER WITH ROLLBACK IMMEDIATE  USE MASTER RESTORE DATABASE SEGUROSUSA FROM DISK = '" + txtRastaurar.Text + "' WITH REPLACE;", Connection.ObtenerConexion());
                restaurar1.ExecuteNonQuery();
                SqlCommand restaurar3 = new SqlCommand("ALTER DATABASE SEGUROSUSA SET MULTI_USER", Connection.ObtenerConexion());
                restaurar3.ExecuteNonQuery();
                MessageBox.Show("Base de datos restaurada");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al restaurar \n" + ex.ToString());
            }
            finally
            {
                Connection.conn.Close();
                btnRestaurar.Enabled = false;
            }
        }
    }
}
