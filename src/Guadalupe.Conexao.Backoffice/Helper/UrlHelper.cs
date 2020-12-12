namespace Guadalupe.Conexao.Backoffice.Helper
{
    public static class UrlHelper
    {
        public static string GetImagePath(string image) 
        {
            return $"{Startup.ApiUrl}{image}";
        }
    }
}
