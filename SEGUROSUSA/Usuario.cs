using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGUROSUSA
{
    class Usuario
    {

        public static List<Usuario> Listausuarios;
        public static Usuario auxiliar;
        public Int32 idUsuario;
        public String nombreCompleto;
        public String usuario;
        public String contrasena;
        public Int32 tipoUsuario;

        public Usuario() { }

        public Usuario(Int32 idUsuario, String nombreCompleto, String usuario, String contrasena,Int32 tipoUsuario)
        {
            this.idUsuario = idUsuario;
            this.nombreCompleto = nombreCompleto;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.tipoUsuario = tipoUsuario;
        }

        public override string ToString()
        {
            return nombreCompleto;
        }
    }
}
