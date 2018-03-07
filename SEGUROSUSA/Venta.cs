using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGUROSUSA
{
    class Venta
    {
        public Int32 idVenta;
        public String usuario;
        public Double monto;
        public Int32 tipoDePago;
        public DateTime fecha;
        public Int32 formaDePago;
        public Double valorDolar;
        public Double montoPesos;

        public Venta() { }

        public Venta(
            Int32 idVenta,
            String usuario,
            Double monto,
            Int32 tipoDePago,
            DateTime fecha,
            Int32 formaDePago,
            Double valorDolar,
            Double montoPesos)
        {
            this.idVenta = idVenta;
            this.usuario = usuario;
            this.monto = monto;
            this.tipoDePago = tipoDePago;
            this.fecha = fecha;
            this.formaDePago = formaDePago;
            this.valorDolar = valorDolar;
            this.montoPesos = montoPesos;
        }
    }
}
