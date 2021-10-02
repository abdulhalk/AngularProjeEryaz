using AngularProje.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AngularProje.Controllers
{
   
    public class ProductController : Controller
    {
       

        Database_acces_Layer.db dblayer = new Database_acces_Layer.db();

        List<Product> products = new List<Product>();

        
        public IActionResult HomePage()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult Get_Product()
        {
            using (SqlConnection con = new SqlConnection("data source=.; database=Proje; integrated security=SSPI"))
            {
                // Insert query  
                //string query = "INSERT INTO student(name,email,contact) VALUES(@name, @email, @contact)";

                // Executing select command  
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Get_Product"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // Retrieving Record from datasource  
                    SqlDataReader sdr = cmd.ExecuteReader();
                    // Reading and Iterating Records  
                    while (sdr.Read())
                    {

                        Product product = new Product();
                        product.ProductID = (int)sdr["ProductID"];
                        product.ProductName = sdr["ProductName"].ToString();
                        product.ProductPrice = Convert.ToSingle(sdr["ProductPrice"]);
                        product.ProductStock = (int)sdr["ProductStock"];
                        product.ProductDetails = sdr["ProductDetails"].ToString();

                        products.Add(product);

                    }
                }
                return new JsonResult(products);

            }
        }


        [HttpPost]
        public JsonResult Add_Product([FromBody] Product product)
        {

            using (SqlConnection con = new SqlConnection("data source=.; database=Proje; integrated security=SSPI"))
            {
                // Insert query  
                //string query = "INSERT INTO student(name,email,contact) VALUES(@name, @email, @contact)";
                using (SqlCommand cmd = new SqlCommand("Add_Product"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // opening connection  
                    con.Open();
                    // Passing parameter values  
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    cmd.Parameters.AddWithValue("@ProductStock", product.ProductStock);
                    cmd.Parameters.AddWithValue("@ProductDetails", product.ProductDetails);
                    // Executing insert query  
                    cmd.ExecuteNonQuery();
                }
                
                return new JsonResult(product);
            }
        }


        public JsonResult Get_databyid(int id)

        {

            DataSet ds = dblayer.get_recordbyid(id);

            List<Product> productlist = new List<Product>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                productlist.Add(new Product

                {

                    ProductID = Convert.ToInt32(dr["ProductID"]),

                    ProductName = dr["ProductName"].ToString(),

                    ProductPrice = Convert.ToInt32(dr["ProductPrice"]),

                    ProductStock = Convert.ToInt32(dr["Name"]),

                    ProductDetails = dr["ProductDetails"].ToString(),

                });

            }

            return new JsonResult(productlist);
        }


        [HttpPost]
        public JsonResult Update_Product([FromBody] Product product)
        {
            string res = string.Empty;
            try
            {
                dblayer.update_record(product);
                res = "Updated";
            }
            catch (Exception)
            {
                res = "failed";
            }
            return new JsonResult(product);
        }





        public JsonResult delete_record(int id)
        {
            string res = string.Empty;
            try
            {
                dblayer.delete_product(id);
                res = "data deleted";
            }
            catch (Exception)
            {
                res = "failed";
            }
            return new JsonResult(res);
        }
    }
}
