using Examen.IServices;
using Examen.Models.Data;
using Examen.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Services
{
    public class HabilidadServices : IHabilidad
    {
        #region variables
        private ApplicationDbContext _Context;
        #endregion
        #region constructor
        public HabilidadServices(ApplicationDbContext Context)
        {
            this._Context = Context;
        }
        #endregion
        public async Task<bool> AddHabilidad(EmpleadoHabilidad _Habilidad)
        {
            try
            {
                await _Context.AddAsync(_Habilidad);
                var resultado = await _Context.SaveChangesAsync();
                return resultado > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<dynamic> GetHabilidad(int id)
        {
            return await _Context.EmpleadoHabilidad.Select(x => new { x.IdEmpleado, x.NombreHabilidad })
                        .Where(x => x.IdEmpleado.Equals(id)).ToListAsync();
        }
    }
}
