using System.ServiceModel;

namespace DBService
{
    [ServiceContract]
    interface ISystem
    {
        [OperationContract]
        bool AddSysInfo(string os, string pcName, string processoeInfo, int idUser);
    }
}
