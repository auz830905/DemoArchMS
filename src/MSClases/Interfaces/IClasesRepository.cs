﻿using MSClases.Models;

namespace MSClases.Interfaces
{
	public interface IClasesRepository
	{
        Task<List<Clase>> GetClases();
        Task<Clase> GetClase(int Id);
        Task<bool> AddClase(Clase clase);
        Task<bool> DeleteClase(int Id);
        Task<Clase> UpdateClase(Clase clase);
    }
}

