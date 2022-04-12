using Examen.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Examen.IServices
{
    public interface IHabilidad
    {
        Task<bool> AddHabilidad(EmpleadoHabilidad _Habilidad);
        Task<dynamic> GetHabilidad(int id);
    }
}
