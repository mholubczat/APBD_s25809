namespace LegacyApp
{
    public class Client
    {
        public string Name { get; internal init; }
        public int ClientId { get; internal init; }
        public string Email { get; internal init; }
        public string Address { get; internal init; }
        public ClientType Type { get; init; }
    }
}