using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ActivityService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ActivityService.svc or ActivityService.svc.cs at the Solution Explorer and start debugging.
    public class ActivityService : IActivity
    {
        public bool SessionDurationStart(int idUser, DateTime whenStart)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"INSERT INTO ks.Activity  (id_user, _from ) VALUES ('{idUser}', '{whenStart}')";
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

        public bool SessionDurationStop(DateTime whenStarted, int idUser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"UPDATE ks.Activity 
                                            SET _to = '{DateTime.Now.ToString()}'
                                            WHERE id_user='{idUser}' AND _from = '{whenStarted.ToString()}'";
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

        public bool UpdateuserStatus(string status, int idUser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"UPDATE  ks.Online 
                                            SET status = '{status}'
                                            WHERE id_user= '{idUser}'";
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
