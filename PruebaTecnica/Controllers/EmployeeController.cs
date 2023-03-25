using PruebaTecnica.Logica;
using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PruebaTecnica.Controllers
{
    
    public class EmployeeController : ApiController
    {
        private LogicaEmployee Logica = new LogicaEmployee();
        /// <summary>
        /// Muestra un listado de los empleados
        /// </summary>
        /// <returns> Listado de Empleados</returns>
        [AcceptVerbs("GET")]
        [Route("api/Employee/MostrarEmployees")]
        public List<EEmployee> MostrarEmployees()
        {
            try
            {               
                return Logica.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Muestra un listado de tipo de empleados
        /// </summary>
        /// <returns> Listado de Empleados</returns>
        [AcceptVerbs("GET")]
        [Route("api/Employee/MostrarTypeEmployees")]
        public List<EEmployeeType> MostrarTypeEmployees()
        {
            try
            {
                return Logica.ListTypeEmploye();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Guarda los empleados
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("api/Employee/GuardarEmployees")]
        public async Task<bool> GuardarEmployees(EEmployee Employe)
        {
            try
            {
                
                return await Logica.Crear(Employe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Editar los empleados
        /// </summary>
        /// <param name="Employe"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("api/Employee/EditarEmployees")]
        public async Task<bool> EditarEmployees(EEmployee Employe)
        {
            try
            {

                return await Logica.Editar(Employe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Eliminar los empleados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("api/Employee/EliminarEmployees")]
        public async Task<bool> EliminarEmployees(EEmployee model)
        {
            try
            {

                return await Logica.Eliminar(model.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
