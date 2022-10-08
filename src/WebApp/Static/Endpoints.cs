namespace WebApp.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:7119";

        public static string ProfesoresList = $"{BaseUrl}/GetProfesores";
        public static string DeleteProfesor = $"{BaseUrl}/DeleteProfesor/";
        public static string ClasesList = $"{BaseUrl}/GetClases";
    }
}
