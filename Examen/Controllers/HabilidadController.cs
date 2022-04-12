using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.IServices;
using Examen.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    public class HabilidadController : Controller
    {
        #region variables
        public IHabilidad _Habilidad;
        #endregion
        #region constructor
        public HabilidadController(IHabilidad HabilidadEm)
        {
            this._Habilidad = HabilidadEm;
        }
        #endregion
        [HttpPost]
        public async Task<bool> AddHabilidad(EmpleadoHabilidad EmHabilidad)
        {
            return await _Habilidad.AddHabilidad(EmHabilidad);
        }
        [HttpGet]
        public async Task<dynamic>GetHabilidad(int id)
        {
            return await _Habilidad.GetHabilidad(id);
        }
    }
}
