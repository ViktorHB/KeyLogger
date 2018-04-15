using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Tests.SercisesTests
{
    [TestFixture]
    class SystemServiceTest
    {
        String conStr;
        int testId;
        ServiceReferenceSystem.SystemClient sys;

        [SetUp]
        public void Setup()
        {
            conStr = StringResources.connectionString;
            sys = new ServiceReferenceSystem.SystemClient();
            AddUser();
            testId = GetTestUserId();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveSysInfo(testId);
            RemoveUser();
        }

        [Test]
        public void AddSysInfoTest()
        {
            Thread.Sleep(3000);
            Assert.IsTrue(sys.AddSysInfo("test", "test", "Test", testId));
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

        private void RemoveSysInfo(int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string commandStr = $@"DELETE ks.System WHERE id_user = '{id}';";
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

    }
}
