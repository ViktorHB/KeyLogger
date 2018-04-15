using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataBaseAdo" in both code and config file together.
    [ServiceContract]
    public interface IDataBaseAdo
    {
        [OperationContract]

        void Start();

        [OperationContract]

        void Stop();

        [OperationContract]

        void Update();
    }
}
