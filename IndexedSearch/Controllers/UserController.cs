using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndexedSearch.Controllers
{
    public class UserController : Controller
    {
        private Models.SearchModel SearchModel = new Models.SearchModel();

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Administrator\\Documents\\GitHub\\Search-Index-Form-On-MVC-.NET\\IndexedSearch\\App_Data\\Users.mdf;Integrated Security=True";
        
        public ActionResult Index()
        {
            return View();
        }
        string SqlGetConnectionString(string ConfigPath, string ConnectionStringName)
        {
            System.Configuration.Configuration rootWebConfig =
                            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConfigPath);
            System.Configuration.ConnectionStringSettings connectionString =
                rootWebConfig.ConnectionStrings.ConnectionStrings[ConnectionStringName];
            if (connectionString == null || string.IsNullOrEmpty(connectionString.ConnectionString))
                throw new Exception("Fatal error: Connection string is missing from web.config file");

            return connectionString.ConnectionString;
        }
        public ActionResult Search(string text)
        {

            using (SqlConnection connection =
                       new SqlConnection(this.SqlGetConnectionString("/Web.Config", "Users")))
            //using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //try
                //{
                    string SqlQuery = @"SELECT dbo.Users.* FROM dbo.Users";
                    if (Request.IsAjaxRequest() && text != "")
                        SqlQuery += " WHERE dbo.Users.Username LIKE @text";

                    SqlCommand command = new SqlCommand(SqlQuery, connection);
                    command.Parameters.AddWithValue("@text", String.Format("%{0}%", text));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read() && reader.HasRows != false)
                        {
                            Models.UsersModel Users = new Models.UsersModel();
                            Users.UserId = Int32.Parse(reader["UserId"].ToString());
                            Users.Username = reader["Username"].ToString();
                            Users.Role = reader["Role"].ToString();
                            Users.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                            Users.Fname = reader["Fname"].ToString();
                            Users.Lname = reader["Lname"].ToString();
                            Users.Department = reader["Department"].ToString();
                            Users.Doj = DateTime.Parse(reader["Doj"].ToString());
                            Users.Mgld = Int32.Parse(reader["Mgld"].ToString());
                            Users.Seniority = Double.Parse(reader["Seniority"].ToString());
                            Users.EmpCode = reader["EmpCode"].ToString();

                            SearchModel.UserList.Add(Users);
                        }

                        reader.Close();
                    }
               //}
                //catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return PartialView(SearchModel.UserList);
        }
        public ActionResult Create(string person, string phone)
        {
            using (SqlConnection connection =
                       new SqlConnection(this.SqlGetConnectionString("/Web.Config", "Users")))
            {
                try
                {
                    string SqlQuery = @"INSERT INTO dbo.Users VALUES (@person, @phone)";
                    SqlCommand command = new SqlCommand(SqlQuery, connection);
                    command.Parameters.AddWithValue("@person", person);
                    command.Parameters.AddWithValue("@phone", phone);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }


            return RedirectToAction("Index");
        }
    }
}