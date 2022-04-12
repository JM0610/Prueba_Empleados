using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.IServices;
using Examen.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    public class AreaController : Controller
    {
        #region variables
        public IArea _Area;
        #endregion
        #region constructor
        public AreaController(IArea Area)
        {
            this._Area = Area;
        }
        #endregion
        public async Task<IActionResult> Index(string busqueda, int pagina =1)
        {
            var PaginaResultado = await _Area.GetAreas(busqueda, pagina);

            return View(PaginaResultado);
        }
        [HttpPost]
        public async Task<bool>AddArea(Area parametros)
        {
            return await _Area.AddArea(parametros);
        }
        [HttpPost]
        public async Task<bool> UpdateArea(Area parametros)
        {
            return await _Area.UpdateArea(parametros);
        }
        [HttpDelete]
        public async Task<bool> DeleteArea(int id)
        {
            return await _Area.DeleteArea(id);
        }
        [HttpGet]
        public async Task<List<Area>> GetAreas()
        {
            return await _Area.GetListArea();
        }
    }
}
