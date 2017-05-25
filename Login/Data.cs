using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using asm3.Models;
using System.Configuration;
namespace Login
{
    class Data
    {
        private string connString;
        public Data()
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public void addUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO user(Username,Password,FirstName,LastName,Type) VALUES(@username,@pass,@fname,@lname,@type)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@pass", user.password);
                cmd.Parameters.AddWithValue("@fname", user.firstname);
                cmd.Parameters.AddWithValue("@lname", user.lastname);
                cmd.Parameters.AddWithValue("@type", user.type);
                cmd.ExecuteNonQuery();
            }
        }
        public User GetUser(string userName)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM user where Username=\"admin\";";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    {
                        User user = new User();
                        user.ID = reader.GetInt32("ID");
                        user.username = reader.GetString("Username");
                        user.password = reader.GetString("Password");
                        user.firstname = reader.GetString("FirstName");
                        user.lastname = reader.GetString("LastName");
                        user.type = reader.GetInt32("Type");
                        return user;
                    }
                }
            }

            return null;
        }
    }
}
