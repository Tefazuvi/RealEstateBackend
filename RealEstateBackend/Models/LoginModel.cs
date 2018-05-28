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

        public int idUser
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public string lastname
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }

        public string phone
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public int type
        {
            get;
            set;
        }

        public string profile
        {
            get;
            set;
        }

        public string Login(string user, string pass)
        {
            string query = "Select * from User where Email='" + user + "' and Password='" + pass + "'";

            MySqlDataReader reader = conexionM.getExecuteQuery(query);

            LoginModel login = new LoginModel();

            string Pictures = "";

            while (reader.Read())
            {
                login.idUser = Int32.Parse(reader["idUser"].ToString());
                login.name = reader["Name"].ToString();
                login.lastname = reader["LastName"].ToString();
                login.email = reader["Email"].ToString();
                login.password = reader["Password"].ToString();
                login.phone = reader["Phone"].ToString();
                login.type = Int32.Parse(reader["Type"].ToString());
                Pictures = reader["idPictures"].ToString();
            }

            if (Pictures != "") {
                int idPictures = Int32.Parse(Pictures);
                ImageFile image = new ImageFile();
                login.profile = image.GetImageFile(idPictures);
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
                lstParams.Add(new ParameterSchema("Name", NewUser.name));
                lstParams.Add(new ParameterSchema("LastName", NewUser.lastname));
                lstParams.Add(new ParameterSchema("Email", NewUser.email));
                lstParams.Add(new ParameterSchema("Password", NewUser.password));
                lstParams.Add(new ParameterSchema("Phone", NewUser.phone));
                lstParams.Add(new ParameterSchema("Type", NewUser.type));

                query = "INSERT INTO User (Name,LastName,Email,Password,Phone,Type) " +
                    "values(@Name,@LastName,@Email,@Password,@Phone,@Type)";

                string ans = conexionM.setExecuteQuery(query, lstParams);

                if (profile != null)
                {
                    ImageFile Image = new ImageFile();
                    Image.SaveImageFile(profile, Int32.Parse(ans));
                }

                return ans;
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }
        /*
               public string DeleteUser(LoginModel user)
               {

                   try
                   {
                       string query = "DELETE FROM User WHERE id=" + user.idUser;
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
               /*
       public string UpdateUser(LoginModel user)
       {
       try
       {
           string query = "UPDATE User set Name='" + user.name + "',LastName='" + user.lastname + "',Email='" + user.email + "',Phone='" + user.phone + "' WHERE id=" + user.idUser;
           string ans = conexionM.deleteExecuteQuery(query);
           return ans;
           //return conexionM.setExecuteQuery(query, lstParams);
       }
       catch (Exception ex)
       {
           //throw ex;
           return ex.Message;
       }
           }*/
    }
}