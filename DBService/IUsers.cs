using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DBService
{
    [ServiceContract]
    interface IUsers 
    {
        [OperationContract]
        bool AddNewUser(string mac);

        [OperationContract]
        bool AddUserAdress(string ip, string userName, int userId);

        [OperationContract]
        int GetUserId(string mac);

        [OperationContract]
        List<String> GetAllUsersMac();
    }
}
