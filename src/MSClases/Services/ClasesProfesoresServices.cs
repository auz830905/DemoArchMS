using System;
using MSClases.Interfaces;
using MSClases.Models;

namespace MSClases.Services
{
    public class ClasesProfesoresServices : IClasesProfesoresRepository
    {
        private readonly DBContextClases _db;

        public ClasesProfesoresServices(DBContextClases db)
        {
            _db = db;
        }

        public Task<bool> AddProfesorAClase(string ci, int IdClase)
        {
            try
            {
                var clase = _db.Clases.FirstOrDefault(c => c.Id == IdClase);

                if (clase != null)
                {
                    var claseProfesor = new ClaseProfesor()
                    {
                        IdClase = IdClase,
                        Ci = ci,
                        IdClaseNavigation = clase
                    };

                    _db.Add<ClaseProfesor>(claseProfesor);
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

        public Task<List<Clase>> GetClasesByProfesor(string ci)
        {
            var clases = (from c in _db.Clases
                          join cp in _db.ClaseProfesors
                          on c.Id equals cp.IdClase
                          where cp.Ci == ci select c).ToList();
            return Task.FromResult(clases);
        }
    }
}

