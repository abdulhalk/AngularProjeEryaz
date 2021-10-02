using AngularProje.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AngularProje.Controllers
{
    
    
    public class LoginController : Controller
    {
        //SqlConnection con = new SqlConnection("data source=.; database=Proje; integrated security=SSPI");

        Database_acces_Layer.db dblayer = new Database_acces_Layer.db();

        List<user_table> users = new List<user_table>();


        public IActionResult Index()
        {
            return View();
        }
        //[AllowAnonymous]
        [HttpGet]
        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public JsonResult userlogin([FromBody] user_table user)
        {
            string result = Convert.ToString(dblayer.userLogin(user));
            if (result == "1")
            {

                //RedirectToAction("Product", "HomePage");
            }
            else
            {
                result = "Mail or Password is wrong";


            }

            return new JsonResult(result);

        }


        [HttpPost]
        public JsonResult Add_User ([FromBody] user_table user)
        {
            using (SqlConnection con = new SqlConnection("data source=.; database=Proje; integrated security=SSPI"))
            {
                using(SqlCommand cmd = new SqlCommand("User_Register"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                    cmd.Parameters.AddWithValue("@UserMail", user.UserMail);
                    cmd.ExecuteNonQuery();
                }
                return new JsonResult(user);
            }
            
        }
    }
}
