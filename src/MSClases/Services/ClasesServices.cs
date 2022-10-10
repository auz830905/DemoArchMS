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
                _db.Clases.Add(clase);
                _db.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteClase(int Id)
        {
            try
            {
                var clase = GetClase(Id).Result;
                if (clase != null)
                {
                    _db.Clases.Remove(clase);
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
            _db.Clases.Update(clase);
            _db.SaveChanges();
            return Task.FromResult(clase);
        }
    }
}

