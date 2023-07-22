using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EmployeeApplication.Models;

namespace EmployeeApplication.DAL
{
    //connect with db and read the data
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["addConnectionstring"].ToString();

        //Get all products
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            //call stored procedure

            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(); 

                con.Open();
                sqlDA.Fill(dt);
                con.Close();

                foreach(DataRow dr in dt.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return productList;
        }


        //insert products

        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("InsertProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName",product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Qty", product.Qty);
                cmd.Parameters.AddWithValue("@Remarks",product.Remarks);


                connection.Open();
                id=cmd.ExecuteNonQuery();
                connection.Close();

            }
            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Get  products by id
        public List<Product> GetProductsByID(int ProductID)
        {
            List<Product> productList = new List<Product>();
            //call stored procedure

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetProductByID";
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                sqlDA.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return productList;
        }



        //update products

        public bool UpdateProduct(Product product)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("UpdateProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Qty", product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", product.Remarks);


                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete product

        public string DeleteProduct(int ProductID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("DeleteProduct", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", ProductID);
                
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    }


}