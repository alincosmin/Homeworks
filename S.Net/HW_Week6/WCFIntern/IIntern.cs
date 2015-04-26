using System.ServiceModel;

namespace WCFIntern
{
    [ServiceContract]
    public interface IIntern
    {
        [OperationContract]
        string SayHello(string msg);

        [OperationContract]
        string GetName();
    }
}
