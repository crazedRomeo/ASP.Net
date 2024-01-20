using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Xml.Linq;

namespace IndexedSearch.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Department { get; set; }
        public DateTime Doj { get; set; }
        public int Mgld { get; set; }
        public double Seniority { get; set; }
        public string EmpCode { get; set; }

        
    }

    public class SearchModel
    {
        private List<UsersModel> user_list = null;
        public SearchModel()
        {
            if (user_list == null)
                user_list = new List<UsersModel>();
        }

        public List<UsersModel> UserList 
            { get { return (user_list != null) ? user_list : null; } }
    }
}