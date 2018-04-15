using System;
using System.ServiceModel;

namespace DBService
{
    [ServiceContract]

    interface IActivity
    {
        [OperationContract]
        bool UpdateuserStatus(String status, int idUser);

        [OperationContract]
        bool SessionDurationStart(int idUser, DateTime whenStart);

        [OperationContract]
        bool SessionDurationStop(DateTime whenStarted, int idUser);
    }
}
