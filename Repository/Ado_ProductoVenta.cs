using ProyectoFinalCoderHouse2022.Models;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse2022.Repository
{
    public class Ado_ProductoVenta
    {
        public static List<ProductoVenta> TraerProductoVendido(int idUsuario)
        {
            var listaProductoVenta = new List<ProductoVenta>();

            SqlConnectionStringBuilder conecctionbuilder = new();
            conecctionbuilder.DataSource = "NIKITODEVSS1";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;

            List<Producto> productos = Ado_Producto.TraerProductoPorUsuario(idUsuario);

            foreach (Producto p in productos)
            {

                using (SqlConnection conecction = new SqlConnection(cs))
                {
                    conecction.Open();
                    SqlCommand cmd = conecction.CreateCommand();
                    cmd.CommandText = "SELECT * FROM ProductoVendido WHERE idProducto = @idProducto";

                    var paramIdProducto = new SqlParameter("idProducto", System.Data.SqlDbType.Int);
                    paramIdProducto.Value = p.Id;

                    cmd.Parameters.Add(paramIdProducto);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var prodven = new ProductoVenta();

                        prodven.Id = Convert.ToInt32(reader.GetValue(0));
                        prodven.Stock = Convert.ToInt32(reader.GetValue(1));
                        prodven.IdProducto = Convert.ToInt32(reader.GetValue(2));
                        prodven.IdVenta = Convert.ToInt32(reader.GetValue(3));

                        listaProductoVenta.Add(prodven);
                    }

                    reader.Close();
                }
            }

            return listaProductoVenta;
        }
    }
}
