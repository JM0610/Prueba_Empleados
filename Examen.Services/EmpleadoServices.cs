using Examen.IServices;
using Examen.Models.Data;
using Examen.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Services
{
    public class EmpleadoServices : IEmpleado
    {
        #region variables
        private ApplicationDbContext _Context;
        private readonly int RegistrosPagina = 5;
        //Lista de datos del modelo
        private List<Empleado> Lista;
        //Tipo de modlo que procesara el paginador
        private PaginadorGenerico<Empleado> Paginador;
        #endregion
        #region constructor
        public EmpleadoServices(ApplicationDbContext Context)
        {
            this._Context = Context;
        }
        #endregion
        public async Task<bool> AddEmpleado(Empleado _Empleado)
        {
            try
            {
                await _Context.AddAsync(_Empleado);
                var resultado=await _Context.SaveChangesAsync();
                return resultado > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmpleado(int Id)
        {
            try
            {
                var valores = await _Context.Empleado.FirstOrDefaultAsync(x => x.IdEmpleado.Equals(Id));
                if (valores != null)
                {
                    _Context.Remove(valores);
                    var resultado = await _Context.SaveChangesAsync();
                    return resultado > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<PaginadorGenerico<Empleado>> GetEmpleados(string busqueda, int IdArea, int pagina)
        {
            try
            {
                int TotalRegistros = 0;

                using (_Context)
                {
                    Lista = await _Context.Empleado
                        .ToListAsync();
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        foreach (var item in busqueda.Split(new char[] { ' ' },
                                     StringSplitOptions.RemoveEmptyEntries))
                        {
                            Lista = Lista.Where(x => x.NombreCompleto.Trim().ToUpper().Contains(busqueda.Trim().ToUpper())).ToList();
                        }
                    }
                    if (IdArea > 0 )
                    {
                        Lista = Lista.Where(x => x.IdArea.Equals(IdArea)).ToList();
                    }
                }

                using (_Context)
                {
                    TotalRegistros = Lista.Count();
                    var TotalPagina = (int)Math.Ceiling((double)TotalRegistros / RegistrosPagina);
                    Lista = Lista.OrderBy(x => x.IdArea)
                                                         .Skip((pagina - 1) * RegistrosPagina)
                                                         .Take(RegistrosPagina)
                                                         .ToList();
                    //total de paginas

                    //Reyenamos el paginador
                    Paginador = new PaginadorGenerico<Empleado>()
                    {
                        RegistrosPorPagina = RegistrosPagina,
                        TotalRegistros = TotalRegistros,
                        TotalPaginas = TotalPagina,
                        PaginaActual = pagina,
                        Busqueda = busqueda,
                        Resultado = Lista
                    };
                }
                return Paginador;
            }
            catch
            {
                return null;
            }
        }
        public async Task<dynamic> GetListEmpleados()
        {
            return await _Context.Empleado.Select(x=> new{x.IdEmpleado,x.NombreCompleto }).ToListAsync();
        }
        public async Task<dynamic> GetDatosGenerales(int id)
        {
            return await _Context.Empleado.Include(x=>x.IdAreaNavigation).Include(x=>x.IdJefeNavigation)
                        .Select(x => new { 
                            Codigo = x.IdEmpleado,
                            x.NombreCompleto,
                            x.Cedula,
                            x.Correo,
                            x.FechaNacimiento,
                            x.FechaIngreso,
                            jefe=x.IdJefeNavigation.NombreCompleto,
                            area=x.IdAreaNavigation.Nombre,
                            edad=(DateTime.Today.Month<=x.FechaNacimiento.Month && DateTime.Today.Day<=x.FechaNacimiento.Day)?
                            (DateTime.Today.Year-x.FechaNacimiento.Year)-1:(DateTime.Today.Year-x.FechaNacimiento.Year),
                            tiempo = (DateTime.Today.Month <= x.FechaIngreso.Month && DateTime.Today.Day <= x.FechaIngreso.Day) ?
                            (DateTime.Today.Year - x.FechaIngreso.Year) - 1 : (DateTime.Today.Year - x.FechaIngreso.Year)
                        })
                        .FirstOrDefaultAsync(x => x.Codigo.Equals(id));
        }
        public async Task<bool> UpdateEmpleado(Empleado _Empleado)
        {
            try
            {
                var valores = await _Context.Empleado.FirstOrDefaultAsync(x => x.IdEmpleado.Equals(_Empleado.IdEmpleado));
                if (valores != null)
                {
                    valores.NombreCompleto = _Empleado.NombreCompleto.Trim();
                    valores.Cedula = _Empleado.Cedula.Trim();
                    valores.Correo = _Empleado.Correo.Trim();
                    valores.FechaIngreso = _Empleado.FechaIngreso;
                    valores.FechaNacimiento = _Empleado.FechaNacimiento;
                    valores.IdJefe = _Empleado.IdJefe;
                    valores.IdArea = _Empleado.IdArea;
                    if (_Empleado.Foto != null)
                    {
                        valores.Foto = _Empleado.Foto;
                    }
                     _Context.Update(valores);
                    var resultado =await _Context.SaveChangesAsync();
                    return resultado > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<dynamic> GetSuperior(int id)
        {
            return await _Context.Empleado.Select(x => new { x.IdEmpleado, x.IdArea, x.NombreCompleto,x.IdJefe }).Where(x => x.IdArea.Equals(id) && x.IdJefe==null).ToListAsync();
                
        }
        public async Task<dynamic> GetInferior(int id) 
        {
            return await _Context.Empleado.Select(x => new { x.IdEmpleado, x.IdArea, x.NombreCompleto, x.IdJefe }).Where(x =>  x.IdJefe.Equals(id)).ToListAsync();
        }
        public async Task<byte[]> GetBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
