using CUBE_Dev_App_Server_API.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace CUBE_Dev_App_Server_API.Services
{
    public static class LoginService
    {
        public static bool HeavyClientLogin(string? username, string? password)
        {
            string sql = @"SELECT * FROM DataUtils
                            WHERE DataKey = 'username' OR DataKey = 'password'";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                return false;
            }

            List<DataUtils> dataUtils = new List<DataUtils>();

            while (reader.Read())
            {
                dataUtils.Add(new DataUtils
                {
                    DataKey = reader.GetString("DataKey"),
                    DataValue = reader.IsDBNull("DataValue") ? null : reader.GetString("DataValue")
                });
            }

            // Récupération des infos
            string? correctUsername = dataUtils.Where(x => x.DataKey == "username").Select(x => x.DataValue).FirstOrDefault();
            string? correctPassword = dataUtils.Where(x => x.DataKey == "password").Select(x => x.DataValue).FirstOrDefault();

            // Retourne true si les informations sont identiques
            if (correctUsername == username
                && correctPassword == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateLoginInfos(string? username, string? password)
        {
            string sql = @"UPDATE DataUtils
                            SET
	                            DataValue = 
		                            CASE WHEN Datakey = 'username' THEN @username
                                    WHEN Datakey = 'password' THEN @password
                                    END";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                if (!DBConnection.ExecuteCommandToDB(command))
                    return false;
            }

            return true;
        }

        public static bool WebsiteLogin(int idClient, string password)
        {
            string sql = $@"SELECT Password FROM Client
                            WHERE IDClient = {idClient}";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                return false;
            }

            if (reader.Read())
            {
                Client client = new Client
                {
                    Password = reader.GetString("Password")
                };

                if (client.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
