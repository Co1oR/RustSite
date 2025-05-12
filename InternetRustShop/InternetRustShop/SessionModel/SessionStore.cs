using ApplicationDomain;

namespace InternetRustShop.SessionModel
{
    public static class SessionStore
    {
        public static Dictionary<string, User> OnlieUsers = new Dictionary<string, User>();
    }
}
