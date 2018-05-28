using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace RealEstateBackend.Models
{
    public class ImageFile
    {
            #region Variables
            Conexion conexionM = new Conexion();
            #endregion

            public ImageFile()
            {
            }

            public string Image
            {
                get;
                set;
            }

        public string SaveImageFile(String NewImage, int userId)
        {
            try
            {
                List<ParameterSchema> lstParams = new List<ParameterSchema>();
                List<ParameterSchema> lstParamsUser = new List<ParameterSchema>();
                string query = string.Empty;

                lstParams.Add(new ParameterSchema("Picture", NewImage));

                query = "INSERT INTO Pictures (Picture) " +
                    "values(@Picture)";

                int idPictures = Int32.Parse(conexionM.setExecuteQuery(query, lstParams));

                string queryUser = "UPDATE User set idPictures = " + idPictures+ " where idUser =" + userId;

                return conexionM.setExecuteQuery(queryUser, lstParamsUser);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /*
            public string SaveRImageFile(Byte[] NewImage, int vehicleID)
            {
                try
                {
                    List<ParameterSchema> lstParams = new List<ParameterSchema>();
                    string query = string.Empty;

                    lstParams.Add(new ParameterSchema("Picture", NewImage));
                    lstParams.Add(new ParameterSchema("Received_id", vehicleID));

                    query = "INSERT INTO Pictures (Picture,Received_id) " +
                        "values(@Picture,@Received_id)";

                    return conexionM.setExecuteQuery(query, lstParams);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message;
                }
            }*/

        public string GetImageFile(int idPictures)
            {

                try
                {
                    string query = "Select Picture from Pictures where idPictures=" + idPictures;

                    MySqlDataReader reader = conexionM.getExecuteQuery(query);

                    while (reader.Read())
                    {
                        Image = reader["Picture"].ToString();
                    }

                    return Image;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        /*
            public ObservableCollection<ImageFile> GetRImageFile(int vehicleID)
            {

                ObservableCollection<ImageFile> lstPictures = new ObservableCollection<ImageFile>();

                try
                {
                    string query = "Select * from Pictures where Received_id='" + vehicleID + "'";

                    MySqlDataReader reader = conexionM.getExecuteQuery(query);

                    while (reader.Read())
                    {
                        lstPictures.Add(new ImageFile { Image = (Byte[])reader["Picture"] });
                    }

                    return lstPictures;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }*/
        }
    }