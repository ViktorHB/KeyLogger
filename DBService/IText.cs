using System.ServiceModel;


namespace DBService
{
    [ServiceContract]
    interface IText
    {
        [OperationContract]
        bool UpdateText(string text, int idUser);
    }
}
