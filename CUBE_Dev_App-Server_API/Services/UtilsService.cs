using MySql.Data.MySqlClient;
using System.Data;

namespace CUBE_Dev_App_Server_API.Services
{
    public static class UtilsService
    {
        #region Default Margin

        public static bool GetDefaultMargin(out string? defaultMargin)
        {
            string sql = @"SELECT DataValue FROM DataUtils
                            WHERE DataKey = 'defaultmargin'";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                defaultMargin = null;
                return false;
            }

            if (reader.Read())
            {
                defaultMargin = reader.IsDBNull("DataValue") ? null : reader.GetString("DataValue");
                return true;
            }

            defaultMargin = null;
            return false;
        }

        public static bool UpdateDefaultMargin(decimal defaultMargin)
        {
            string sql = @"UPDATE DataUtils
                            SET DataValue = @defaultmargin
                            WHERE DataKey = 'defaultmargin'";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("@defaultmargin", defaultMargin);

                if (!DBConnection.ExecuteCommandToDB(command))
                    return false;
            }

            return true;
        }

        #endregion

        #region Default TVA

        public static bool GetDefaultTVA(out string? defaultTVA)
        {
            string sql = @"SELECT DataValue FROM DataUtils
                            WHERE DataKey = 'defaulttva'";

            using MySqlDataReader? reader = DBConnection.ExecuteReader(sql);

            if (reader == null)
            {
                defaultTVA = null;
                return false;
            }

            if (reader.Read())
            {
                defaultTVA = reader.IsDBNull("DataValue") ? null : reader.GetString("DataValue");
                return true;
            }

            defaultTVA = null;
            return false;
        }

        public static bool UpdateDefaultTVA(decimal defaultTVA)
        {
            string sql = @"UPDATE DataUtils
                            SET DataValue = @defaulttva
                            WHERE DataKey = 'defaulttva'";

            using (MySqlCommand command = new MySqlCommand(sql))
            {
                command.Parameters.AddWithValue("@defaulttva", defaultTVA);

                if (!DBConnection.ExecuteCommandToDB(command))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
