using Examen.Models.Data;
using Examen.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Examen.IServices
{
    public interface IEmpleado
    {
        Task<bool> AddEmpleado(Empleado _Empleado);
        Task<PaginadorGenerico<Empleado>> GetEmpleados(string busqueda, int IdArea, int pagina);
        Task<dynamic> GetListEmpleados();
        Task<bool> DeleteEmpleado(int Id);
        Task<bool> UpdateEmpleado(Empleado _Empleado);
        Task<byte[]> GetBytes(IFormFile formFile);
        Task<dynamic> GetDatosGenerales(int id);
        Task<dynamic> GetSuperior(int id);
        Task<dynamic> GetInferior(int id);
    }
}
