using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Web.Helper
{
    public class LlamadaServicioHelper
    {
        /// <summary>
        /// Realiza una petición GET a un servicio en específico
        /// </summary>
        /// <param name="Uri"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<string> Get(string Uri, string Url)
        {
            using (System.Net.Http.HttpClient Cliente = new System.Net.Http.HttpClient())
            {
                Cliente.BaseAddress = new Uri(Uri);
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (System.Net.Http.HttpResponseMessage RespuestaApi = await Cliente.GetAsync(Url))
                {
                    if (RespuestaApi.IsSuccessStatusCode)
                    {
                        string ContentResult = await RespuestaApi.Content.ReadAsStringAsync();

                        return ContentResult;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Realiza una petición POST a un servicio en específico
        /// </summary>
        /// <param name="Uri"></param>
        /// <param name="Url"></param>
        /// <param name="Objeto"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<string> Post(string Uri, string Url, object Objeto)
        {
            using (System.Net.Http.HttpClient Cliente = new System.Net.Http.HttpClient())
            {
                Cliente.BaseAddress = new Uri(Uri);
                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (System.Net.Http.HttpResponseMessage RespuestaApi = await Cliente.PostAsJsonAsync(Url, Objeto))
                {
                    if (RespuestaApi.IsSuccessStatusCode)
                    {
                        string ContentResult = await RespuestaApi.Content.ReadAsStringAsync();

                        return ContentResult;
                    }
                    return null;
                }
            }
        }
    }
}