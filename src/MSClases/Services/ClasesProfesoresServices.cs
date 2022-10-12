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

        public Task<ClaseProfesor> AddProfesorAClase(string Ci, int IdClase)
        {
            ClaseProfesor? claseProfesor = null;

            var clase = _db.Clases.FirstOrDefault(c => c.Id == IdClase);

            if (clase != null)
            {
                claseProfesor = new ClaseProfesor()
                {
                    IdClase = IdClase,
                    Ci = Ci,
                    IdClaseNavigation = clase
                };               
                
                var claseProfesorResposnse = _db.ClaseProfesors.Add(claseProfesor).Entity;
                _db.SaveChanges();

                return Task.FromResult(claseProfesorResposnse);
            }

            return Task.FromResult(claseProfesor!);           
        }

        public Task<ClaseProfesor> DeleteClaseProfesor(string Ci, int IdClase)
        {
            ClaseProfesor? clase = null;

            clase = _db.ClaseProfesors.FirstOrDefault(c => c.IdClase == IdClase && c.Ci == Ci);

            if (clase != null)
            {
                var claseProfesorResponse =  _db.ClaseProfesors.Remove(clase).Entity;
                _db.SaveChanges();

                return Task.FromResult(claseProfesorResponse);
            }
       
            return Task.FromResult(clase!);           
        }

        public Task<bool> DeleteClaseProfesor(string Ci)
        {           
            var listaClasePorProfesor = _db.ClaseProfesors.Where(cp => cp.Ci == Ci).ToList();

            if (listaClasePorProfesor != null && listaClasePorProfesor.Count != 0)
            {                    
                _db.ClaseProfesors.RemoveRange(listaClasePorProfesor);
                _db.SaveChanges();    
                
                return Task.FromResult(true);
            }

            return Task.FromResult(false);            
        }

        public Task<List<Clase>> GetClasesByProfesor(string ci)
        {
            var clases = (from c in _db.Clases
                          join cp in _db.ClaseProfesors
                          on c.Id equals cp.IdClase
                          where cp.Ci == ci select c).ToList();
            return Task.FromResult(clases);
        }

        public Task<List<Clase>> GetClasesNotAsinadasAProfesor(string ci)
        {
            var clasesAsigandas = GetClasesByProfesor(ci).Result.Select(c => c.Id).ToList();
            var clasesSinAsignar = (from c in _db.Clases where !clasesAsigandas.Contains(c.Id) select c).ToList();
            return Task.FromResult(clasesSinAsignar);
        }
    }
}

