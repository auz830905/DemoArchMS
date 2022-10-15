using MSClases.Models;

namespace MSClases.Interfaces
{
	public interface IClasesRepository
	{
        Task<List<Clase>> GetClases();
        Task<Clase> GetClase(int Id);
        Task<Clase> AddClase(Clase clase);
        Task<Clase> DeleteClase(int Id);
        Task<Clase> UpdateClase(Clase clase);
    }
}