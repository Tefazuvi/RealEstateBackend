using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace RealEstateBackend.Models
{
    public class LoginModel
    {
        #region Variables
        Conexion conexionM = new Conexion();
        #endregion

        public LoginModel()
        {
        }

        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Login(string user, string pass)
        {
            string query = "Select * from User where Email='" + user + "' and Password='" + pass + "'";

            MySqlDataReader reader = conexionM.getExecuteQuery(query);

            LoginModel login = new LoginModel();

            while (reader.Read())
            {
                login.UserID = Int32.Parse(reader["ID"].ToString());
                login.Email = reader["Email"].ToString();
                login.Password = reader["Password"].ToString();
                login.Type = Int32.Parse(reader["Type"].ToString());
                login.Name = reader["Name"].ToString();
                login.LastName = reader["LastName"].ToString();
                login.Phone = reader["Phone"].ToString();
            }

            var json = JsonConvert.SerializeObject(login);

            return json;
        }

        public string SaveUser(LoginModel NewUser)
        {
            try
            {
                List<ParameterSchema> lstParams = new List<ParameterSchema>();
                string query = string.Empty;

                lstParams.Add(new ParameterSchema("Email", NewUser.Email));
                lstParams.Add(new ParameterSchema("Password", NewUser.Password));
                lstParams.Add(new ParameterSchema("Type", NewUser.Type));
                lstParams.Add(new ParameterSchema("Name", NewUser.Name));
                lstParams.Add(new ParameterSchema("LastName", NewUser.LastName));
                lstParams.Add(new ParameterSchema("Phone", NewUser.Phone));

                query = "INSERT INTO User (Email,Password,Type,Name,LastName,Phone) " +
                    "values(@Email,@Password,@Type,@Name,@LastName,@Phone)";

                return conexionM.setExecuteQuery(query, lstParams);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }

        public string DeleteUser(LoginModel user)
        {
            try
            {
                string query = "DELETE FROM User WHERE id=" + user.UserID;
                string ans = conexionM.deleteExecuteQuery(query);
                return ans;
                //return conexionM.setExecuteQuery(query, lstParams);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }

        public string UpdateUser(LoginModel user)
        {
            try
            {
                string query = "UPDATE User set Name='" + user.Name + "',LastName='" + user.LastName + "',Email='" + user.Email + "',Phone='" + user.Phone + "' WHERE id=" + user.UserID;
                string ans = conexionM.deleteExecuteQuery(query);
                return ans;
                //return conexionM.setExecuteQuery(query, lstParams);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }
    }
}