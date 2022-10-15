namespace WebApp.Static
{
    public static class Endpoints
    {
        private static readonly string BaseUrl = "https://localhost:7119";

        public static readonly string ProfesoresList = $"{BaseUrl}/gateway/profesores";
        public static readonly string Profesor = $"{BaseUrl}/gateway/profesores/";
        public static readonly string AddProfesor = $"{BaseUrl}/gateway/profesores";
        public static readonly string DeleteProfesor = $"{BaseUrl}/gateway/profesores/";
        public static readonly string UpdateProfesor = $"{BaseUrl}/gateway/profesores";

        public static readonly string ClasesList = $"{BaseUrl}/gateway/clases";
        public static readonly string Clase = $"{BaseUrl}/gateway/clases/";
        public static readonly string AddClase = $"{BaseUrl}/gateway/clases";
        public static readonly string DeleteClase = $"{BaseUrl}/gateway/clases/";
        public static readonly string UpdateClase = $"{BaseUrl}/gateway/clases";

        public static readonly string ClasesNotAssinedProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static readonly string ClasesByProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static readonly string AddClaseProfesor = $"{BaseUrl}/gateway/clasesprofesores/";
        public static readonly string DeleteClasesProfesor = $"{BaseUrl}/gateway/clasesprofesores/";

        public static readonly string UserRegister = $"{BaseUrl}/singin";
        public static readonly string UserLogin = $"{BaseUrl}/gateway/users/login";
    }
}