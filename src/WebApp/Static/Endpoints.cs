namespace WebApp.Static
{
    public static class Endpoints
    {
        private static string BaseUrl = "https://192.168.49.2:30461";

        public static string ProfesoresList = $"{BaseUrl}/gateway/profesores";
        public static string Profesor = $"{BaseUrl}/gateway/profesores/";
        public static string AddProfesor = $"{BaseUrl}/gateway/profesores";
        public static string DeleteProfesor = $"{BaseUrl}/gateway/profesores/";
        public static string UpdateProfesor = $"{BaseUrl}/gateway/profesores";

        public static string ClasesList = $"{BaseUrl}/gateway/clases";
        public static string Clase = $"{BaseUrl}/gateway/clases/";
        public static string AddClase = $"{BaseUrl}/gateway/clases";
        public static string DeleteClase = $"{BaseUrl}/gateway/clases/";
        public static string UpdateClase = $"{BaseUrl}/gateway/clases";

        public static string ClasesNotAssinedProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static string ClasesByProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static string AddClaseProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static string DeleteClasesProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
    }
}
