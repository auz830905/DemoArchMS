namespace WebApp.Static
{
    public static class Endpoints
    {
        public static readonly string ProfesoresList = "/gateway/profesores";
        public static readonly string Profesor = "/gateway/profesores/";
        public static readonly string AddProfesor = "/gateway/profesores";
        public static readonly string DeleteProfesor = "/gateway/profesores/";
        public static readonly string UpdateProfesor = "/gateway/profesores";

        public static readonly string ClasesList = "/gateway/clases";
        public static readonly string Clase = "/gateway/clases/";
        public static readonly string AddClase = "/gateway/clases";
        public static readonly string DeleteClase = "/gateway/clases/";
        public static readonly string UpdateClase = "/gateway/clases";

        public static readonly string ClasesNotAssinedProfesor = "/gateway/clasesprofesores/";
        public static readonly string ClasesByProfesor = "/gateway/clasesprofesores/";
        public static readonly string AddClaseProfesor = "/gateway/clasesprofesores/";
        public static readonly string DeleteClasesProfesor = "/gateway/clasesprofesores/";

        public static readonly string UserRegister = "/singin";
        public static readonly string UserLogin = "/gateway/users/login";
    }
}