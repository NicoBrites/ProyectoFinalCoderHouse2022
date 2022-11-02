using ProyectoFinalCoderHouse2022.Models;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse2022.Repository
{
    public class Ado_Producto
    {
        public static List<Producto> TraerProductoPorUsuario(int idUsuario)
        {
            var listaProductos = new List<Producto>();


            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();
                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto where IdUsuario = @idUsu";

                var paramIdUsu = new SqlParameter("idUsu", System.Data.SqlDbType.Int);
                paramIdUsu.Value = idUsuario;

                cmd.Parameters.Add(paramIdUsu);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var prod = new Producto();

                    prod.Id = Convert.ToInt32(reader.GetValue(0));
                    prod.Descripcion = reader.GetValue(1).ToString();
                    prod.Costo = Convert.ToInt32(reader.GetValue(2));
                    prod.PrecioVenta = Convert.ToInt32(reader.GetValue(3));
                    prod.Stock = Convert.ToInt32(reader.GetValue(4));
                    prod.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(prod);
                }

                reader.Close();

                return listaProductos;
            }
        }
        public static List<Producto> TraerProductos()
        {
            var listaProductos = new List<Producto>();


            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();
                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "SELECT * FROM producto";

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var prod = new Producto();

                    prod.Id = Convert.ToInt32(reader.GetValue(0));
                    prod.Descripcion = reader.GetValue(1).ToString();
                    prod.Costo = Convert.ToInt32(reader.GetValue(2));
                    prod.PrecioVenta = Convert.ToInt32(reader.GetValue(3));
                    prod.Stock = Convert.ToInt32(reader.GetValue(4));
                    prod.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(prod);
                }

                reader.Close();

                return listaProductos;
            }
        }
        public static void CrearProducto(Producto prod)
        {

            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "Insert into Producto ( Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                                   "values ( @descripcionesProd, @costoProd, @precioVentaProd, @stockProd, @idUsuarioProd)";



                var paramDescripcionProd = new SqlParameter("descripcionesProd", System.Data.SqlDbType.VarChar);
                paramDescripcionProd.Value = prod.Descripcion;

                var paramCostoProd = new SqlParameter("costoProd", System.Data.SqlDbType.Money);
                paramCostoProd.Value = prod.Costo;

                var paramPrecioVentaProd = new SqlParameter("precioVentaProd", System.Data.SqlDbType.Money);
                paramPrecioVentaProd.Value = prod.PrecioVenta;

                var paramStockProd = new SqlParameter("stockProd", System.Data.SqlDbType.Int);
                paramStockProd.Value = prod.Stock;

                var paramIdUsuarioProd = new SqlParameter("idUsuarioProd", System.Data.SqlDbType.BigInt);
                paramIdUsuarioProd.Value = prod.IdUsuario;

                cmd.Parameters.Add(paramDescripcionProd);
                cmd.Parameters.Add(paramCostoProd);
                cmd.Parameters.Add(paramPrecioVentaProd);
                cmd.Parameters.Add(paramStockProd);
                cmd.Parameters.Add(paramIdUsuarioProd);
                cmd.ExecuteNonQuery();
            }
        }
        public static void ModificarProducto(Producto prod)
        {


            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();

                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "UPDATE Producto SET Descripciones = @descripcionesProd, Costo = @costoProd, PrecioVenta = @precioVentaProd, Stock = @stockProd, IdUsuario = @idUsuarioProd " +
                                   "WHERE ID = @idProd";


                var paramIdProd = new SqlParameter("idProd", System.Data.SqlDbType.BigInt);
                paramIdProd.Value = prod.Id;

                var paramDescripcionProd = new SqlParameter("descripcionesProd", System.Data.SqlDbType.VarChar);
                paramDescripcionProd.Value = prod.Descripcion;

                var paramCostoProd = new SqlParameter("costoProd", System.Data.SqlDbType.Money);
                paramCostoProd.Value = prod.Costo;

                var paramPrecioVentaProd = new SqlParameter("precioVentaProd", System.Data.SqlDbType.Money);
                paramPrecioVentaProd.Value = prod.PrecioVenta;

                var paramStockProd = new SqlParameter("stockProd", System.Data.SqlDbType.Int);
                paramStockProd.Value = prod.Stock;

                var paramIdUsuarioProd = new SqlParameter("idUsuarioProd", System.Data.SqlDbType.BigInt);
                paramIdUsuarioProd.Value = prod.IdUsuario;


                cmd.Parameters.Add(paramIdProd);
                cmd.Parameters.Add(paramDescripcionProd);
                cmd.Parameters.Add(paramCostoProd);
                cmd.Parameters.Add(paramPrecioVentaProd);
                cmd.Parameters.Add(paramStockProd);
                cmd.Parameters.Add(paramIdUsuarioProd);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteProducto(int idProd)
        {
            using (SqlConnection conecction = new SqlConnection(Connection.connectionString()))
            {
                conecction.Open();
                SqlCommand cmd = conecction.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductoVendido WHERE idProducto = @idProd ;\n DELETE FROM Producto WHERE id = @idProd";


                var paramIdProd = new SqlParameter("idProd", System.Data.SqlDbType.BigInt);
                paramIdProd.Value = idProd;

                cmd.Parameters.Add(paramIdProd);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
