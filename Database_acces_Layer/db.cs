using AngularProje.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AngularProje.Database_acces_Layer
{
    
    public class db
    {
        SqlConnection con = new SqlConnection("data source=.; database=Proje; integrated security=SSPI");

        public void delete_product(int id)
        {
            SqlCommand com = new SqlCommand("Delete_Product", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@ProductID", id);

            con.Open();

            com.ExecuteNonQuery();

            con.Close();
        }

        public void update_record(Product product)

        {

            SqlCommand com = new SqlCommand("Update_Product", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@ProductID", product.ProductID);

            com.Parameters.AddWithValue("@ProductName", product.ProductName);

            com.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);

            com.Parameters.AddWithValue("@ProductStock", product.ProductStock);

            com.Parameters.AddWithValue("@ProductDetails", product.ProductDetails);

            con.Open();

            com.ExecuteNonQuery();

            con.Close();

        }

        // Get Record by id

        public DataSet get_recordbyid(int id)

        {

            SqlCommand com = new SqlCommand("Get_Product_byID", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@ProductID", id);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        public int userLogin(user_table user)
        {
            SqlCommand com = new SqlCommand("User_Login", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@UserMail", user.UserMail);
            com.Parameters.AddWithValue("@UserPassword", user.UserPassword);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.Direction = ParameterDirection.Output;
            oblogin.SqlDbType = SqlDbType.Bit;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            con.Close();
            return res;

        }        
    }
}
