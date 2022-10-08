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

        public Task<bool> AddClase(Clase clase)
        {
            try
            {
                _db.Add<Clase>(clase);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<Clase> GetClase(int Id)
        {
            var clase = _db.Clases.FirstOrDefault(c => c.Id == Id);
            return Task.FromResult(clase!);
        }

        public Task<List<Clase>> GetClases()
        {
            var clases = _db.Clases.ToList<Clase>();
            return Task.FromResult(clases);
        }
    }
}

