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

        public Task<Profesor> AddProfesor(Profesor profesor)
        {
            var profeResponse = _db.Profesores.Add(profesor).Entity;
            _db.SaveChanges();
            return Task.FromResult(profeResponse);            
        }

        public Task<Profesor> DeleteProfesor(string Ci)
        {            
            var profesor = GetProfesor(Ci).Result;
            if (profesor != null)
            {
                _db.Profesores.Remove(profesor);
                _db.SaveChanges();
                return Task.FromResult(profesor);
            }
            return Task.FromResult(new Profesor());            
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

        public Task<Profesor> UpdateProfesor(Profesor profesor)
        {
            var profesorResponse = GetProfesor(profesor.CI).Result;

            if (profesorResponse != null)
            {
                profesorResponse.Nombre = profesor.Nombre;
                profesorResponse.Apellidos = profesor.Apellidos;

                var profesorUdpate = _db.Profesores.Update(profesorResponse).Entity;
                _db.SaveChanges();

                return Task.FromResult(profesorUdpate);
            }   
            return Task.FromResult(new Profesor());
        }
    }
}