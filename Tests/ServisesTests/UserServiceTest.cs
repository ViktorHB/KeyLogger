using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Tests.SercisesTests
{
    [TestFixture]
    class UserServiceTest
    {
        String conStr;
        int testId;
        ServiceReferenceUser.UsersClient user;

        [SetUp]
        public void Setup()
        {
            conStr = StringResources.connectionString;
            user = new ServiceReferenceUser.UsersClient();
            AddUser();
            testId = GetTestUserId("TestUser");
        }

        [TearDown]
        public void TearDown()
        {
            RemoveUser("TestUser", testId);
        }

        [Test]
        public void AddNewUserTest()
        {
            Assert.AreEqual(true, user.AddNewUser("TestUser1"));
            RemoveUser("TestUser1", GetTestUserId("TestUser1"));
        }
        [Test]
        public void AddUserAdressTest()
        {
            Assert.AreEqual(true, user.AddUserAdress("ip", "name", testId));
            RemoveUserAdress(testId);
        }

        [Test]
        public void GetUserIdTest()
        {
            Assert.AreEqual(testId, user.GetUserId("TestUser"));
        }

        [Test]
        public void GetAllUsersTestCompareUsersNames()
        {
            Assert.AreEqual(GetAllUsersNames(), user.GetAllUsersMac());
            //to comparethe collections 
            CollectionAssert.AreEqual(GetAllUsersNames(), user.GetAllUsersMac());
        }


        [Test]
        public void GetAllUsersTestUsersCount()
        {
            Assert.AreEqual(GetAllUsersNames().Count(), user.GetAllUsersMac().Count());
        }
     
        private List<String> GetAllUsersNames()
        {
            using (SqlConnection con = new SqlConnection(conStr))
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
        private void AddUser()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"INSERT INTO ks.Users (physical_adress) VALUES ('TestUser');";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void RemoveUser(String name, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@" DELETE ks.Online WHERE id_user  = '{id}';
                                        DELETE ks.Users WHERE physical_adress = '{name}';";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void RemoveUserAdress(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@" DELETE ks.UserAdress WHERE id_user  = '{id}';";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private int GetTestUserId(String userName)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"SELECT TOP 1 id FROM ks.Users WHERE physical_adress='{userName}'";
                SqlCommand command = new SqlCommand(commandStr, con);
                var res = command.ExecuteScalar();
                command.Dispose();
                return (int)res;
            }
        }
        private void RemoveText(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"DELETE ks.Text WHERE id_user = '{id}';";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
    }
}
