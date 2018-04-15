using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SystemService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SystemService.svc or SystemService.svc.cs at the Solution Explorer and start debugging.
    public class SystemService : ISystem
    {
        public bool AddSysInfo(string os, string pcName, string processoeInfo, int idUser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"IF '{os}' NOT IN (SELECT ks.System.os FROM ks.System)
	                                            OR '{pcName}' NOT IN (SELECT ks.System.pc_name FROM ks.System)
	                                            OR '{processoeInfo}' NOT IN (SELECT ks.System.pc_name FROM ks.System)
                                            BEGIN
	                                            INSERT INTO ks.System  (os, pc_name ,processor_identifier,id_user) VALUES ('{os}','{pcName}', '{processoeInfo}','{idUser}')
                                            END";
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
