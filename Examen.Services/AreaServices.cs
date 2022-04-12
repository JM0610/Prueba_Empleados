using Microsoft.EntityFrameworkCore;
using Examen.IServices;
using Examen.Models.Data;
using Examen.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Examen.Services
{
    public class AreaServices : IArea
    {
        #region variables
        public ApplicationDbContext _Context;
        private readonly int RegistrosPagina = 5;
        //Lista de datos del modelo
        private List<Area> Lista;
        //Tipo de modlo que procesara el paginador
        private PaginadorGenerico<Area> Paginador;
        #endregion
        #region constructor
        public AreaServices(ApplicationDbContext Context)
        {
            this._Context = Context;
        }
        #endregion
        public async Task<bool> AddArea(Area _Area)
        {
            try
            {
                await _Context.AddAsync(_Area);
                var flag = await _Context.SaveChangesAsync();
                return flag > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteArea(int Id)
        {
            try
            {
                var valores = await _Context.Area.FirstOrDefaultAsync(x => x.IdArea.Equals(Id));
                if (valores != null)
                {
                    _Context.Remove(valores);
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

        public async Task<PaginadorGenerico<Area>> GetAreas(string busqueda, int pagina)
        {
            try
            {
                int TotalRegistros = 0;

                using (_Context)
                {
                    Lista = await _Context.Area
                        .ToListAsync();
                    if (!string.IsNullOrEmpty(busqueda))
                    {
                        foreach (var item in busqueda.Split(new char[] { ' ' },
                                     StringSplitOptions.RemoveEmptyEntries))
                        {
                            Lista = Lista.Where(x => x.Nombre.Trim().ToUpper().Contains(busqueda.Trim().ToUpper())).ToList();
                        }
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
                    Paginador = new PaginadorGenerico<Area>()
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

        public async Task<bool> UpdateArea(Area _Area)
        {
            try
            {
                var valores = await _Context.Area.FirstOrDefaultAsync(x => x.IdArea.Equals(_Area.IdArea));
                if (valores != null)
                {
                    valores.Nombre = _Area.Nombre;
                    valores.Descripcion = _Area.Descripcion;
                    _Context.Update(valores);
                    var resultado=await _Context.SaveChangesAsync();
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
        public async Task<List<Area>> GetListArea()
        {
            return await _Context.Area.ToListAsync();
        }
    }
}
