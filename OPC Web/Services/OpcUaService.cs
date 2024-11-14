using Opc.UaFx.Client;

namespace OPC_Web.Services
{
    public class OpcUaService : IDisposable
    {
        private OpcClient _opcClient;

        public void Connect(string ServerUrl)
        {
            _opcClient = new OpcClient(ServerUrl);
            _opcClient.Connect();
        }

        public void Disconnect()
        {
            _opcClient.Disconnect();
        }

        public Opc.UaFx.OpcValue ReadNode(string tag)
        {
            return _opcClient.ReadNode(tag);
        }

        public void WriteNode(string tag, object value)
        {
            _opcClient.WriteNode(tag, value);
        }


        public void Dispose()
        {
            Disconnect();
            _opcClient?.Dispose();
        }
    }
}
