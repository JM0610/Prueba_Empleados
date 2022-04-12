using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen.IServices;
using Examen.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    public class EmpleadoController : Controller
    {
        #region variables
        public IEmpleado _Empleado;
        #endregion
        #region constructor
        public EmpleadoController(IEmpleado Empleado)
        {
            this._Empleado = Empleado;
        }
        #endregion
        public async Task<IActionResult> Index(string busqueda,int idArea, int pagina = 1)
        {
            ViewData["valor"] = idArea;
            var PaginaResultado = await _Empleado.GetEmpleados(busqueda,idArea, pagina);
            return View(PaginaResultado);
        }
        public IActionResult formulario_general()
        {
            return View();
        }
        [HttpPost]
        public async Task<bool>AddEmpleado(Empleado parametros)
        {
            if (parametros.FotoAdj != null)
            {
                parametros.Foto = await _Empleado.GetBytes(parametros.FotoAdj);
            }
            return await _Empleado.AddEmpleado(parametros);
        }
        [HttpPost]
        public async Task<bool> UpdateEmpleado(Empleado parametros)
        {
            if (parametros.FotoAdj != null)
            {
                parametros.Foto = await _Empleado.GetBytes(parametros.FotoAdj);
            }
            return await _Empleado.UpdateEmpleado(parametros);
        }
        [HttpDelete]
        public async Task<bool> DeleteEmpleado(int id)
        {
            return await _Empleado.DeleteEmpleado(id);
        }
        [HttpGet]
        public async Task<dynamic> GetListEmpleado()
        {
            return await _Empleado.GetListEmpleados();
        }
        [HttpGet]
        public async Task<dynamic> GetDatosGenerales(int id)
        {
            return await _Empleado.GetDatosGenerales(id);
        }
        [HttpGet]
        public async Task<dynamic> GetSuperior(int id)
        {
            return await _Empleado.GetSuperior(id);
        }
        [HttpGet]
        public async Task<dynamic> GetInferior(int id)
        {
            return await _Empleado.GetInferior(id);
        }
    }
}
