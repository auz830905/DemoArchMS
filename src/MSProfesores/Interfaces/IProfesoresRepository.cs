using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSProfesores.Models;

namespace MSProfesores.Interfaces
{
    public interface IProfesoresRepository
    {
        Task<List<Profesor>> GetProfesores();
        Task<Profesor> GetProfesor(string Ci);
        Task<bool> AddProfesor(Profesor profesor);
    }
}