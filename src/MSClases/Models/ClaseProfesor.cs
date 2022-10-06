using System;
using System.Numerics;

namespace MSClases.Models
{
	public class ClaseProfesor
	{
		public string Ci { get; set; } 
		public int IdClase { get; set; }
		public virtual Clase IdClaseNavigation { get; set; } = null!;
    }
}

