namespace WebApp.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:7119";

        public static string ProfesoresList = $"{BaseUrl}/gateway/profesores";
        public static string DeleteProfesor = $"{BaseUrl}/gateway/profesores";
        public static string ClasesList = $"{BaseUrl}/GetClases";
    }
}
