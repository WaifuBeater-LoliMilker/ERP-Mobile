namespace ERP_Mobile.Services
{
    public interface IApiSettings
    {
        public void SaveServerSettings(string ip, int port);
        public (string ip, int port) LoadServerSettings();
    }
    public class ApiSettings : IApiSettings
    {
        private const string IpKey = "ServerIP";
        private const string PortKey = "ServerPort";

        public void SaveServerSettings(string ip, int port)
        {
            Preferences.Set(IpKey, ip);
            Preferences.Set(PortKey, port);
        }

        public (string ip, int port) LoadServerSettings()
        {
            string ip = Preferences.Get(IpKey, "192.168.1.2");
            int port = Preferences.Get(PortKey, 8088);
            return (ip, port);

        }
    }
}
