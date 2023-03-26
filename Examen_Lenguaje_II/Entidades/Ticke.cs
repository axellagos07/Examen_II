using System;

namespace Entidades
{
    public class Ticke
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ISV { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string IdentidadCliente { get; set; }
        public string CodigoUsuario { get; set; }

        public Ticke()
        {
        }

        public Ticke(int id, DateTime fecha, string descripcion, int cantidad, decimal precio, decimal subTotal, decimal iSV, decimal descuento, decimal total, string identidadCliente, string codigoUsuario)
        {
            Id = id;
            Fecha = fecha;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            SubTotal = subTotal;
            ISV = iSV;
            Descuento = descuento;
            Total = total;
            IdentidadCliente = identidadCliente;
            CodigoUsuario = codigoUsuario;
        }
    }
}
