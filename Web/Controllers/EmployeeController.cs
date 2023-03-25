using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Helper;
using Web.Models;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        string url = "http://localhost:53161/";
        public async Task<ActionResult> Index()
        {
            List<EmployeeViewModel> list = new List<EmployeeViewModel>();
            string respuesta = await LlamadaServicioHelper.Get(url, "api/Employee/MostrarEmployees");
            if (respuesta != "")
            {
                await ListarTipoEmpleado();
                list = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(respuesta);
            }
            return View(list);
        }

        public async Task<List<EmployeeTypeViewModel>> ListarTipoEmpleado()
        {
            List<EmployeeTypeViewModel> list = new List<EmployeeTypeViewModel>();
            string respuesta = await LlamadaServicioHelper.Get(url, "api/Employee/MostrarTypeEmployees");
            if (respuesta != "")
            {
                list = JsonConvert.DeserializeObject<List<EmployeeTypeViewModel>>(respuesta);
                ViewBag.ListaTipoEmpleado=list;
            }
            return list;
        }

        [HttpPost]
        public async Task<ActionResult> GuardarEmployee(EmployeeViewModel model)
        {
            if (model.Id==0)
            {
                if (ModelState.IsValid)
                {
                    string respuesta = await LlamadaServicioHelper.Post(url, "api/Employee/GuardarEmployees", model);
                    bool respuestaJson = JsonConvert.DeserializeObject<bool>(respuesta);
                    if (respuestaJson) { return Json(new { success = true }); }
                    else
                    {
                        return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
                    }

                }
                else
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
                }
            }
            else
            {
                return await EditarEmployee(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditarEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string respuesta = await LlamadaServicioHelper.Post(url, "api/Employee/EditarEmployees", model);
                bool respuestaJson = JsonConvert.DeserializeObject<bool>(respuesta);
                if (respuestaJson) { return Json(new { success = true }); }
                else
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
                }

            }
            else
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EliminarEmpleado(int employeeId)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employeeId;
            if (employeeId != 0)
            {
                string respuesta = await LlamadaServicioHelper.Post(url, "api/Employee/EliminarEmployees", model);
                bool respuestaJson = JsonConvert.DeserializeObject<bool>(respuesta);
                if (respuestaJson) { return Json(new { success = true }); }
                else
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
                }

            }
            else
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
            }
        }
    }
}