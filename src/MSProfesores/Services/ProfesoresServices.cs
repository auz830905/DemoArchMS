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
                _db.Profesores.Add(profesor);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteProfesor(string Ci)
        {
            try
            {
                var profesor = GetProfesor(Ci).Result;
                if (profesor != null)
                {
                    _db.Profesores.Remove(profesor);
                    _db.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
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