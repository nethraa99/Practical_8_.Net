using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Practical_8_.Net.Models
{
    public class InventoryDataAccess
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ConnectionString;

        // READ ALL (SELECT)
        public List<InventoryItem> GetAllProducts()
        {
            var list = new List<InventoryItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Quantity, Price FROM Products";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new InventoryItem
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // READ BY ID
        public InventoryItem GetProductById(int id)
        {
            InventoryItem product = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Quantity, Price FROM Products WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new InventoryItem
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                        }
                    }
                }
            }
            return product;
        }

        // CREATE (INSERT)
        public bool InsertProduct(InventoryItem item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductName, Quantity, Price) VALUES (@ProductName, @Quantity, @Price)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // UPDATE
        public bool UpdateProduct(InventoryItem item)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, Quantity = @Quantity, Price = @Price WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                    cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // DELETE
        public bool DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}