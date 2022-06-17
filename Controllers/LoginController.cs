using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SJCollegeLogin.Models;

namespace SJCollegeLogin.Controllers
{
    public class LoginController : ApiController
    {
        UserModel userModel = new UserModel();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        // POST: Login/Create
        public UserModel Loginuser(UserModel userModel)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserTable WHERE Email = @email and Password=@password", con);
            cmd.Parameters.Add(new SqlParameter("@email", userModel.Email));
            cmd.Parameters.Add(new SqlParameter("@password", userModel.Password));
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                userModel.Batch = dr["Batch"].ToString();
                userModel.Name = dr["Name"].ToString();
                userModel.Role = dr["Role"].ToString();
                userModel.Id = Convert.ToInt32(dr["ID"]); 
            }
            else
            {
                userModel.Role = null;
            }
            con.Close();
            return userModel;
        }

        
    }
}
