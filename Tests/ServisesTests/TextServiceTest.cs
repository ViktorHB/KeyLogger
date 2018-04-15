using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Tests.SercisesTests
{
    [TestFixture]
    class TextServiceTest
    {
        String conStr;
        int testId;
        ServiceReferenceText.TextClient text;

        [SetUp]
        public void Setup()
        {
            conStr = StringResources.connectionString;
            text = new ServiceReferenceText.TextClient();
            AddUser();
            testId = GetTestUserId();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveText(testId);
            RemoveUser();
        }

        [Test]
        public void UpdateTextTest()
        {
            Thread.Sleep(3000);
            Assert.AreEqual(true, text.UpdateText("test", testId));
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
        private void RemoveText(int id )
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
