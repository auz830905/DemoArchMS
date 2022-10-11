using MSProfesores.Models;

namespace MSProfesores.Interfaces
{
    public interface IProfesoresRepository
    {
        Task<List<Profesor>> GetProfesores();
        Task<Profesor> GetProfesor(string Ci);
        Task<Profesor> AddProfesor(Profesor profesor);
        Task<Profesor> DeleteProfesor(string Ci);
        Task<Profesor> UpdateProfesor(Profesor profesor);
    }
}