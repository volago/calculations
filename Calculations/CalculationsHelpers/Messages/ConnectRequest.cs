namespace CalculationsHelpers.Messages
{
    public sealed class ConnectRequest
    {
        public ConnectRequest(string clientAddress)
        {
            ClientAddress = clientAddress;
        }

        public string ClientAddress { get; private set; }
    }
}
