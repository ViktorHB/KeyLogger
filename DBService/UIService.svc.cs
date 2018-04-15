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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UIService.svc or UIService.svc.cs at the Solution Explorer and start debugging.
    public class UIService : IUIService
    {


        public List<string> GetAllMessages(string userName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $"SELECT date,text FROM ks.Text WHERE id_user = (SELECT id FROM ks.Users WHERE physical_adress = '{userName}')";
                    SqlCommand command = new SqlCommand(commandStr, con);
                    var reader = command.ExecuteReader();
                    List<String> tmp_users = new List<string>();

                    while (reader.Read())
                        tmp_users.Add($"{reader[0].ToString()}\n{reader[1].ToString()}\n\n");

                    command.Dispose();
                    return tmp_users;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> GetAllUsers()
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

        public List<string> GetMessegesOnDate(string userName, DateTime date)
        {
            //??? 
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"SELECT text FROM ks.Text 
                                            WHERE
                                            id_user = (SELECT TOP 1 id FROM ks.Users WHERE physical_adress = '{userName}')
											AND 
                                            date > '{date}'";
                  //  date = '{date.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
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

        public List<string> LiveSearch(string key)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
                {
                    con.Open();
                    string commandStr = $@"SELECT physical_adress FROM ks.Users 
                                            WHERE
                                            physical_adress LIKE '%{key}'
                                            OR physical_adress LIKE '%{key}%'
                                            OR physical_adress LIKE '{key}%'";
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
    }
}
