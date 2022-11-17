namespace sqlapp1.Services
{
    using sqlapp1.Models;
    using System.Data.SqlClient;

    public class ProductService
    {
        private static string db_source = "appserver201.database.windows.net";
        private static string db_user = "swatikhori";
        private static string db_password = "Vin@microsoft123";
        private static string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var connectionBuider = new SqlConnectionStringBuilder();
            connectionBuider.DataSource = db_source;
            connectionBuider.UserID = db_user;
            connectionBuider.Password = db_password;
            connectionBuider.InitialCatalog = db_database;

            return new SqlConnection(connectionBuider.ConnectionString);
        }
        public List<Product> GetProducts()
        {
            SqlConnection connection = GetConnection();
            List<Product> products = new List<Product>();

            string statement = "Select ProductID, ProductName, Quantity from Products";
            connection.Open();

            SqlCommand cmd = new SqlCommand(statement, connection);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductID = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    products.Add(product);
                }
            }
            connection.Close();
            return products;            
        }
    }
}
