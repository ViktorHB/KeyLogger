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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataBaseAdo" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataBaseAdo.svc or DataBaseAdo.svc.cs at the Solution Explorer and start debugging.
    public class DataBaseAdo : IDataBaseAdo
    {
        public bool AddNewUser(string mac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"INSERT INTO ks.Users (physical_adress) VALUES ('{mac}')";
                    SqlCommand comAddUser = new SqlCommand(commandStr, con);
                    comAddUser.ExecuteNonQuery();
                    comAddUser.Dispose();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddUserAdress(string ip, string userName, int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"INSERT INTO ks.UserAdress  (ip_adress, name ,id_user ) VALUES ('{ip}','{userName}', '{userId}')";
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

        public int GetAllUsersMac()
        {
            throw new NotImplementedException();
        }

        public int GetUserId(string mac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"SELECT id FROM ks.Users  WHERE name = '{mac}'";
                    SqlCommand command = new SqlCommand(commandStr, con);
                    var res = command.ExecuteScalar();
                    command.Dispose();
                    return (int)res;
                }

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

      
    }
}
