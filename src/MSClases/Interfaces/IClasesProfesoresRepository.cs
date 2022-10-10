using MSClases.Models;

namespace MSClases.Interfaces
{
	public interface IClasesProfesoresRepository
	{
        Task<List<Clase>> GetClasesByProfesor(string ci);
        Task<List<Clase>> GetClasesNotAsinadasAProfesor(string ci);
        Task<bool> AddProfesorAClase(string Ci, int IdClase);
        Task<bool> DeleteClaseProfesor(string Ci, int IdClase);
        Task<bool> DeleteClaseProfesor(string Ci);
    }
}

