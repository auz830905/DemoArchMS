using Microsoft.EntityFrameworkCore;
using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Services
{
	public class ClasesServices : IClasesRepository
	{
        private readonly DBContextClases _db;

        public ClasesServices(DBContextClases db)
		{
            _db = db;
		}

        public Task<Clase> AddClase(Clase clase)
        {
            var claseResponse = _db.Clases.Add(clase).Entity;           
            _db.SaveChanges();
            return Task.FromResult(claseResponse);            
        }

        public Task<Clase> DeleteClase(int Id)
        {           
            var clase = _db.Clases.Include(c => c.ClaseProfesor).Where(c => c.Id == Id).FirstOrDefault(); 

            if (clase != null)
            {
                foreach (var claseProfesor in clase.ClaseProfesor)
                {
                    _db.Remove(claseProfesor);
                }
                _db.Clases.Remove(clase);
                _db.SaveChanges();
                return Task.FromResult(clase);
            }
            return Task.FromResult(clase!);           
        }

        public Task<Clase> GetClase(int Id)
        {
            var clase = _db.Clases.FirstOrDefault(c => c.Id == Id);
            return Task.FromResult(clase!);
        }

        public Task<List<Clase>> GetClases()
        {
            var clases = _db.Clases.ToList();
            return Task.FromResult(clases);
        }

        public Task<Clase> UpdateClase(Clase clase)
        {
            var claseResponse = GetClase(clase.Id).Result;

            if (claseResponse != null)
            {
                claseResponse.Nombre = clase.Nombre;
                
                var claseUpdate = _db.Clases.Update(claseResponse).Entity;
                _db.SaveChanges();

                return Task.FromResult(claseUpdate);
            }
            
            return Task.FromResult(claseResponse!);
        }
    }
}

