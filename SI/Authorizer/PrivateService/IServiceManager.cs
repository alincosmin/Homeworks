using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PrivateService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceManager" in both code and config file together.
    [ServiceContract]
    public interface IServiceManager
    {
        [OperationContract]
        string InitialConnect(Guid id);

        [OperationContract]
        string[] RequestToAccessService(string message);
    }
}
