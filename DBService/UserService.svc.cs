using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUsers
    {
        public bool AddNewUser(string mac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"IF '{mac}' NOT IN (SELECT ks.Users.physical_adress FROM ks.Users)
                                                BEGIN
	                                                INSERT INTO ks.Users (physical_adress) VALUES ('{mac}');
                                                    INSERT INTO ks.Online (id_user, status) VALUES ((SELECT id FROM ks.Users WHERE ks.Users.physical_adress = '{mac}'),'online');
                                                END;";
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
                    string commandStr = $@"IF '{ip}' NOT IN (SELECT ks.UserAdress.ip_adress FROM ks.UserAdress) OR '{userName}' NOT IN (SELECT ks.UserAdress.name FROM ks.UserAdress)
                                            BEGIN
	                                            INSERT INTO ks.UserAdress  (ip_adress, name ,id_user ) VALUES ('{ip}','{userName}', '{userId}')
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


        public List<String> GetAllUsersMac()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"SELECT physical_adress FROM ks.Users";
                    SqlCommand command = new SqlCommand(commandStr, con);
                    var reader = command.ExecuteReader();
                    List<String> tmp_users = new List<string>();

                    while (reader.Read())
                        tmp_users.Add(reader[0].ToString());

                    command.Dispose();
                    return tmp_users;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetUserId(string mac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"SELECT id FROM ks.Users  WHERE physical_adress = '{mac}'";
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
    }
}
