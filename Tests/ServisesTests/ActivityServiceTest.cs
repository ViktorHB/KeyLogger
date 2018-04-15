using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Tests.SercisesTests
{
    [TestFixture]
    class ActivityServiceTest
    {
        ServiceReferenceActivity.ActivityClient act;
        String conStr;
        int testId;

        [SetUp]
        public void Setup()
        {
            act = new ServiceReferenceActivity.ActivityClient();
            conStr = StringResources.connectionString;

            AddUser();
            testId = GetTestUserId();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveUser();
        }

        [Test]
        public void UpdateuserStatusTest()
        {
            Thread.Sleep(3000);
            AddStatus("test");
            act.UpdateuserStatus("xxx", testId);
            String tmp_str = GetUserStatus(testId);
            Assert.IsTrue(act.UpdateuserStatus("xxx", testId));
            Assert.AreEqual(tmp_str, GetUserStatus(testId));
            RrmoveStatus(testId);
        }

        [Test]
        public void SessionDurationStartTest()
        {
            act.SessionDurationStart(testId, DateTime.Now);
            string tmp_str = GetDataStringFromDb("_from", testId);
            Assert.AreEqual(tmp_str, GetDataStringFromDb("_from", testId));
            RemoveTestActivity(testId);
        }


        [Test]
        public void SessionDurationStopTEst()
        {
            act.SessionDurationStop(DateTime.Now, testId);
            String tmp_str = GetDataStringFromDb("_to", testId);
            Assert.AreEqual(tmp_str, GetDataStringFromDb("_to", testId));
            RemoveTestActivity(testId);
        }
        //TODO: Use mock data

        private String GetUserStatus(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $"SELECT TOP 1 status FROM ks.Online WHERE id_user = {id}";
                SqlCommand command = new SqlCommand(commandStr, con);
                var reader = command.ExecuteReader();
                String tmp = null;
                while (reader.Read())
                    tmp = (reader[0].ToString());
                command.Dispose();
                return tmp;
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

        private void AddStatus(String status)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"INSERT INTO ks.Online 
                                    (id_user, status) 
                                    VALUES
                                    ((SELECT id FROM ks.Users WHERE ks.Users.physical_adress = 'TestUser'), '{status}')";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void RrmoveStatus(int Id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"DELETE ks.Online WHERE id_user = '{Id}'";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
        private void RemoveUser()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"DELETE ks.Users WHERE physical_adress = 'TestUser';";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void RemoveTestActivity(int userId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"DELETE ks.Activity WHERE id_user = '{userId}';";
                SqlCommand command = new SqlCommand(commandStr, con);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }
        private int GetTestUserId()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"SELECT TOP 1 id FROM ks.Users WHERE physical_adress='TestUser'";
                SqlCommand command = new SqlCommand(commandStr, con);
                var res = command.ExecuteScalar();
                command.Dispose();
                return (int)res;
            }
        }

        private String GetDataStringFromDb(String col, int user_ID)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $"SELECT TOP 1 {col} FROM ks.Activity WHERE id_user = {user_ID}";
                SqlCommand command = new SqlCommand(commandStr, con);
                var reader = command.ExecuteReader();
                String tmp = null;
                while( reader.Read())
                    tmp = (reader[0].ToString());
                command.Dispose();
                return tmp;
            }
        }
    }
}

