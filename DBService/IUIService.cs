using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUIService" in both code and config file together.
    [ServiceContract]
    public interface IUIService
    {
        [OperationContract]
        List<String> LiveSearch(String key);

        [OperationContract]
        List<String> GetAllUsers();

        [OperationContract]
        List<String> GetAllMessages(String userName);

        [OperationContract]
        List<String> GetMessegesOnDate(String userName, DateTime date);
    }
}
