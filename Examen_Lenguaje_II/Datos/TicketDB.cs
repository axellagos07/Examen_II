using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class TicketDB
    {

        string cadena = "server=localhost; user=root; database=examen2; password=654321";

        public bool Guardar(Ticke tike, List<Ticke> detalles)
        {
            bool inserto = false;
            int idTicke = 0;

            try
            {
                StringBuilder sqlFactura = new StringBuilder();
                sqlFactura.Append(" INSERT INTO factura (Fecha, Descripcion, Cantidad, Precio, SubTotal, ISV, Descuento,Total,IdentidadCliente,CodigoUsuario) VALUES (@Fecha, @Descripcion, @Cantidad, @Precio, @SubTotal, @ISV, @Descuento,@Total,@IdentidadCliente,@CodigoUsuario); ");
                sqlFactura.Append(" SELECT LAST_INSERT_ID(); ");

                //StringBuilder sqlDetalle = new StringBuilder();
                //sqlDetalle.Append(" INSERT INTO detallefactura (IdFactura, CodigoProducto, Precio, Cantidad, Total) VALUES (@IdFactura, @CodigoProducto, @Precio, @Cantidad, @Total); ");

                //StringBuilder sqlExistencia = new StringBuilder();
                //sqlExistencia.Append(" UPDATE producto SET Existencia = Existencia - @Cantidad WHERE Codigo = @Codigo; ");

                using (MySqlConnection con = new MySqlConnection(cadena))
                {
                    con.Open();

                    MySqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    try
                    {
                        using (MySqlCommand cmd1 = new MySqlCommand(sqlFactura.ToString(), con, transaction))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            cmd1.Parameters.Add("@Fecha", MySqlDbType.DateTime).Value = tike.Fecha;
                            cmd1.Parameters.Add("@IdentidadCliente", MySqlDbType.VarChar, 50).Value = tike.IdentidadCliente;
                            cmd1.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 50).Value = tike.CodigoUsuario;
                            cmd1.Parameters.Add("@ISV", MySqlDbType.Decimal).Value = tike.ISV;
                            cmd1.Parameters.Add("@Descuento", MySqlDbType.Decimal).Value = tike.Descuento;
                            cmd1.Parameters.Add("@SubTotal", MySqlDbType.Decimal).Value = tike.SubTotal;
                            cmd1.Parameters.Add("@Total", MySqlDbType.Decimal).Value = tike.Total;
                            idTicke = Convert.ToInt32(cmd1.ExecuteScalar());

                        }



                        transaction.Commit();
                        inserto = true;

                    }
                    catch (System.Exception)
                    {
                        inserto = false;
                        transaction.Rollback();


                    }


                }

            }
            catch (System.Exception)
            {

            }
            return inserto;
        }


    }
}
