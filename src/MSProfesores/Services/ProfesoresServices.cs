using Microsoft.EntityFrameworkCore;
using MSProfesores.Interfaces;
using MSProfesores.Models;

namespace MSProfesores.Services
{
    public class ProfesoresServices : IProfesoresRepository
    {
        private readonly DBContextProfesores _db;

        public ProfesoresServices(DBContextProfesores db)
        {
            _db = db;
        }

        public Task<bool> AddProfesor(Profesor profesor)
        {
            try { 
                _db.Add<Profesor>(profesor);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<Profesor> GetProfesor(string Ci)
        {
            var profesor = _db.Profesores.FirstOrDefault(p => p.CI == Ci);
            return Task.FromResult(profesor!);
        }

        public Task<List<Profesor>> GetProfesores()
        {
            var profesores = _db.Profesores.ToList<Profesor>();
            return Task.FromResult(profesores);
        }
    }
}