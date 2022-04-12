
using Examen.Models.Data;
using Examen.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Examen.IServices
{
    public interface IArea
    {
        Task<bool> AddArea(Area _Area);
        Task<PaginadorGenerico<Area>> GetAreas(string busqueda,int pagina);
        Task<bool> DeleteArea(int Id);
        Task<bool> UpdateArea(Area _Area);
        Task<List<Area>> GetListArea();
    }
}
