using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using asm3.Models;
using System.Configuration;
namespace asm3.Providers
{
    public class DataProvide
    {
        private string connString;

        public DataProvide()
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
                string statement = "SELECT * FROM user where Username= \""+userName+"\";";

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
        public void deleteUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM user WHERE ID = @id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.ExecuteNonQuery();
            }
        }
        public void addConsult(Consultatie cons)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO consultations(IDPatient,IDDoctor,schedule) VALUES(@IDPatient,@IDDoctor,@schedule)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@IDPatient", cons.IDPatient);
                cmd.Parameters.AddWithValue("@IDDoctor", cons.IDDoctor);
                cmd.Parameters.AddWithValue("@schedule", cons.schedule);
                cmd.ExecuteNonQuery();
            }
        }

        public void updateConsult(Consultatie cons)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE consultations SET IDPatient=@IDPatient, IDDoctor=@IDDoctor, schedule=@schedule WHERE ID = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", cons.ID);
                cmd.Parameters.AddWithValue("@IDPatient", cons.IDPatient);
                cmd.Parameters.AddWithValue("@IDDoctor", cons.IDDoctor);
                cmd.Parameters.AddWithValue("@schedule", cons.schedule);
                cmd.ExecuteNonQuery();
            }
        }
        public void addPatient(Patient p)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO patients(IDCode,FirstName,LastName,BirthDate,Address) VALUES(@IDCode,@FirstName,@LastName,@BirthDate,@Address)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@IDCode", p.idCode);
                cmd.Parameters.AddWithValue("@FirstName", p.firstName);
                cmd.Parameters.AddWithValue("@LastName", p.lastName);
                cmd.Parameters.AddWithValue("@BirthDate", p.birthDate);
                cmd.Parameters.AddWithValue("@Address", p.address);
                cmd.ExecuteNonQuery();
            }
        }
        public void updatePatient(Patient p)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE patients SET IDCode=@IDCode, FirstName=@FirstName, LastName=@LastName, BirthDate=@BirthDate, Address=@Address WHERE ID = @id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@IDCode", p.idCode);
                cmd.Parameters.AddWithValue("@FirstName", p.firstName);
                cmd.Parameters.AddWithValue("@LastName", p.lastName);
                cmd.Parameters.AddWithValue("@BirthDate", p.birthDate);
                cmd.Parameters.AddWithValue("@Address", p.address);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE user SET Username=@username, Password=@pass, FirstName=@fname, LastName=@lname, Type=@type WHERE ID = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@pass", user.password);
                cmd.Parameters.AddWithValue("@fname", user.firstname);
                cmd.Parameters.AddWithValue("@lname", user.lastname);
                cmd.Parameters.AddWithValue("@type", user.type);
                cmd.ExecuteNonQuery();
            }
        }
        public IList<User> getUsers()
        {
            IList<User> userList = new List<User>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM user";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.ID = reader.GetInt32("ID");
                        user.username = reader.GetString("Username");
                        user.password = reader.GetString("Password");
                        user.lastname = reader.GetString("LastName");
                        user.firstname = reader.GetString("FirstName");
                        user.type = reader.GetInt32("Type");
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }
        public IList<Consultatie> getConsultatie(int id)
        {
            IList<Consultatie> consultationsList = new List<Consultatie>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand cmd;
                conn.Open();
                if (id != 0)
                {
                    string statement = "SELECT * FROM consultations WHERE ID=" + id;

                    cmd = new MySqlCommand(statement, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                }
                else
                {
                    string statement = "SELECT * FROM consultations";

                    cmd = new MySqlCommand(statement, conn);
                    
                }
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Consultatie c = new Consultatie();
                        c.ID = reader.GetInt32("ID");
                        c.IDPatient = reader.GetInt32("IDPatient");
                        c.IDDoctor = reader.GetInt32("IDDoctor");
                        c.schedule = reader.GetDateTime("schedule");
                        consultationsList.Add(c);
                    }
                }
            }
            return consultationsList;
        }
        public IList<int> getConsultatieID(string id)
        {
            IList<int> consultationsList = new List<int>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand cmd;
                conn.Open();
                 string statement = "SELECT ID FROM consultations WHERE IDDoctor=" + id;

                cmd = new MySqlCommand(statement, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int c;
                        c = reader.GetInt32("ID");
                        consultationsList.Add(c);
                    }
                }
            }
            return consultationsList;
        }
        public void deleteConsultatie(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {


                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete FROM consultations where ID=@id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        
    }


}