using Microsoft.Extensions.Logging;
using ProyectoFinalCoderHouse2022.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse2022.Repository
{
    public class Ado_Venta
    {
        public static Dictionary<Venta, List<Producto>> TraerVentas()
        {
            var listaVenta = new List<Venta>();
            TraerVenta TraerVenta = new TraerVenta();
            var  listaVentaId = new List<Venta>();
            var listaProducto = new List<Producto>();
            var Dicc = new Dictionary<Venta, List<Producto>>();
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();
                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "SELECT * FROM Venta";


                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var venta = new Venta();

                    venta.Id = Convert.ToInt32(reader.GetValue(0));
                    venta.Comentarios = reader.GetValue(1).ToString();
                    venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));



                    listaVenta.Add(venta);
                }
               
                foreach (Venta venta1 in listaVenta)
                {
                    SqlCommand cmd2 = conecction.CreateCommand();
                    cmd2.CommandText = "Select p.Descripciones\r\n" +
                        "from producto as p\r\n" +
                        "INNER JOIN ProductoVendido as pv\r\n" +
                        "on (p.Id = pv.IdProducto)\r\n" +
                        "INNER JOIN Venta as v\r\n" +
                        "on (pv.IdVenta= @ventaId )\n" 
                        ;

                    var paramVentaId = new SqlParameter("ventaId", System.Data.SqlDbType.Int);
                    paramVentaId.Value = venta1.Id;

                    cmd.Parameters.Add(paramVentaId);

                    var reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        var producto = new Producto();

                        producto.Descripcion = reader.GetValue(0).ToString();
                        listaProducto.Add(producto);

                        Dicc.Add(venta1, listaProducto);
                    }
                }
                   



                  //  TraerVenta.Venta.Add(venta);



                

                return Dicc;



                /*
                foreach (Venta venta1 in listaVenta)
                {
                    TraerVenta.Venta.Add(venta1);
                }
             *//*
                    reader.Close();
                conecction.Close();

                SqlCommand cmd1 = conecction.CreateCommand();
                cmd1.CommandText = "SELECT id FROM Venta";

                var reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    var venta = new Venta();

                    venta.Id = Convert.ToInt32(reader.GetValue(0));
                    venta.Comentarios = reader.GetValue(1).ToString();
                    venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));

                    listaVentaId.Add(venta);

                }

                foreach (Venta ventaId in listaVentaId)
                {
                    SqlCommand cmd2 = conecction.CreateCommand();
                    cmd2.CommandText = "Select p.Descripciones\r\n" +
                        "from producto as p\r\n" +
                        "INNER JOIN ProductoVendido as pv\r\n" +
                        "on (p.Id = pv.IdProducto)\r\n" +
                        "INNER JOIN Venta as v\r\n" +
                        "on (pv.IdVenta= @ventaId )";

                    var paramVentaId = new SqlParameter("ventaId", System.Data.SqlDbType.Int);
                    paramVentaId.Value = ventaId;

                    cmd.Parameters.Add(paramVentaId);

                    var reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        var producto = new Producto();

                        producto.Descripcion = reader.GetValue(0).ToString();


                        TraerVenta.Producto.Insert(ventaId);
                    }/*
                    foreach (Producto producto2 in listaProducto)
                    {
                        TraerVenta.Producto.Add(producto2);
                    }

                }
                return TraerVenta;

*/
            }
          






        }
        public static void CargarVenta(CrearVenta venta)
        {
            long idVenta;

            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "Insert into Venta ( Comentarios, IdUsuario)" +
                                   "values (@comentariosVenta, @idUsuarioVenta);" +
                                   "Select scope_identity();";


                var paramComentariosVenta = new SqlParameter("comentariosVenta", System.Data.SqlDbType.VarChar);
                paramComentariosVenta.Value = venta.Venta.Comentarios;

                var paramIdUsuarioVenta = new SqlParameter("idUsuarioVenta", System.Data.SqlDbType.BigInt);
                paramIdUsuarioVenta.Value = venta.Venta.IdUsuario;

                cmd.Parameters.Add(paramComentariosVenta);
                cmd.Parameters.Add(paramIdUsuarioVenta);
                idVenta = Convert.ToInt64(cmd.ExecuteScalar());


                foreach (ProductoVenta producto in venta.ProductosVendidos)
                {

                    SqlCommand cmd1 = conecction.CreateCommand();
                    cmd1.CommandText = "Insert into ProductoVendido(Stock, IdProducto, IdVenta)" +
                                       "values (@stockProdVendido, @idProd, @idVenta)\n" +
                                       "UPDATE Producto SET Stock = Stock - @stockProdVendido WHERE ID = @idProd";


                    var paramStockProdVendido = new SqlParameter("stockProdVendido", System.Data.SqlDbType.Int);
                    paramStockProdVendido.Value = producto.Stock;

                    var paramIdProd = new SqlParameter("idProd", System.Data.SqlDbType.BigInt);
                    paramIdProd.Value = producto.IdProducto;

                    var paramIdVenta = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt);
                    paramIdVenta.Value = idVenta;


                    cmd1.Parameters.Add(paramStockProdVendido);
                    cmd1.Parameters.Add(paramIdProd);
                    cmd1.Parameters.Add(paramIdVenta);
                    cmd1.ExecuteNonQuery();


                }
            }
        }
        /*Eliminar Venta: Recibe una venta con su número de Id, debe buscar en la base de Productos Vendidos cuáles lo tienen eliminándolos, 
         * sumar el stock a los productos incluidos, y eliminar la venta de la base de datos.*/

        public static void EliminarVenta(int idVenta)
        {
            var listaProductoVendido = new List<ProductoVenta>();

            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "Select * FROM ProductoVendido WHERE idVenta = @idVenta";



                var paramIdVentaa = new SqlParameter("idVenta", System.Data.SqlDbType.VarChar);
                paramIdVentaa.Value = idVenta;

                cmd.Parameters.Add(paramIdVentaa);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var productoVendido = new ProductoVenta();

                    productoVendido.Id = Convert.ToInt32(reader.GetValue(0));
                    productoVendido.Stock = Convert.ToInt32(reader.GetValue(1));
                    productoVendido.IdProducto = Convert.ToInt32(reader.GetValue(2));
                    productoVendido.IdVenta = Convert.ToInt32(reader.GetValue(3));

                    listaProductoVendido.Add(productoVendido);
                }

                reader.Close();

                foreach (ProductoVenta producto in listaProductoVendido)
                {

                    SqlCommand cmd1 = conecction.CreateCommand();
                    cmd1.CommandText = "UPDATE Producto SET Stock = Stock + @stockProdVendido WHERE ID = @idProd " +
                        "DELETE FROM ProductoVendido WHERE idVenta = @idVenta ;\n DELETE FROM Venta WHERE id = @idVenta";


                    var paramIdProd = new SqlParameter("idProd", System.Data.SqlDbType.BigInt);
                    paramIdProd.Value = producto.IdProducto;
                    var paramStock = new SqlParameter("stockProdVendido", System.Data.SqlDbType.BigInt);
                    paramStock.Value = producto.Stock;
                    var paraIdVenta = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt);
                    paraIdVenta.Value = idVenta;

                    cmd1.Parameters.Add(paramIdProd);
                    cmd1.Parameters.Add(paramStock);
                    cmd1.Parameters.Add(paraIdVenta);
                    cmd1.ExecuteNonQuery();
                }
            }
        }



        // Debe traer todas las ventas de la base, incluyendo sus Productos, cuya información está en ProductosVendidos.
        /*
        public static TraerVenta TraerVentas1()
        {
            TraerVenta trerVenta = new TraerVenta();

            trerVenta = TraerVentas();


            return trerVenta;


    

        }*/
        }
    }

