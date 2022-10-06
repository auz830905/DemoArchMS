using System;
using MSClases.Models;

namespace MSClases.Interfaces
{
	public interface IClasesProfesoresRepository
	{
        Task<List<Clase>> GetClasesByProfesor(string ci);
        Task<bool> AddProfesorAClase(string ci, int IdClase);
    }
}

