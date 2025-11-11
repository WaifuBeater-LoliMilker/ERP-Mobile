namespace ERP_Mobile.Services
{
    public interface IApiSettings
    {
        public void SaveServerSettings(string address);
        public string LoadServerSettings();
    }
    public class ApiSettings : IApiSettings
    {
        private const string Address = "BaseURLAddress";

        public void SaveServerSettings(string address)
        {
            Preferences.Set(Address, address);
        }

        public string LoadServerSettings()
        {
            string address = Preferences.Get(Address, "http://10.20.29.65:8088/rerpapi/");
            return address;

        }
    }
}
