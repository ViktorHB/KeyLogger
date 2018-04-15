using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TextService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TextService.svc or TextService.svc.cs at the Solution Explorer and start debugging.
    public class TextService : IText
    {
        public bool UpdateText(string text, int idUser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"INSERT INTO ks.Text  (id_user, text, date ) VALUES ('{idUser}','{text}', '{DateTime.Now}')";
                    SqlCommand command = new SqlCommand(commandStr, con);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
}
