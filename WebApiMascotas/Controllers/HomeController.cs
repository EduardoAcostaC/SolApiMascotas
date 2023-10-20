using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApiMascotas.Models;

namespace WebApiMascotas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Mascotas> ls = new List<Mascotas>();
            try
            {
                using (HttpClient clienteHttp = new HttpClient())//instancia del servicio rest
                {

                    clienteHttp.BaseAddress = new Uri("http://localhost:52611/");//URL de donde va a consumir el servicio rest(EndPoint)

                    var request = clienteHttp.GetAsync("api/Values").Result;
                    if (request.IsSuccessStatusCode)
                    {
                        string resultString = request.Content.ReadAsStringAsync().Result;//regresa un Json en un tipo string (Serealizar)

                        //****instalar del nuget newtonsoft.Json
                        ls = JsonConvert.DeserializeObject<List<Mascotas>>(resultString);

                    }
                    else
                    {
                        throw new Exception("Error al ejecutar el API Mascotas");
                    }

                }

                return View(ls);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("ls");
            }
        }

        public ActionResult Create()
        {
            return View("");
        }

        [HttpPost]
        public ActionResult Create(Mascotas mascota)
        {

            try
            {

                using (HttpClient api = new HttpClient())
                {
                    // api.BaseAddress = new Uri("http://localhost:2731/"); 
                    //instalar la libreria del nuget system.Net.Formatting.Extension
                    //Para poder utilizar las entidades como parametros
                    //  var respuesta = api.PostAsync("api/Personas", p,new  JsonMediaTypeFormatter()).Result;

                    var postTask = api.PostAsJsonAsync<Mascotas>("http://localhost:52611/api/Values", mascota);

                    if (postTask.Result.IsSuccessStatusCode)
                    {
                        TempData["mensaje"] = "Se ha agregado correctamente";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        throw new Exception("Error al agregar mascota");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            Mascotas mascota = new Mascotas();
            try
            {
                using (HttpClient clienteHttp = new HttpClient())//instancia del servicio rest
                {
                    clienteHttp.BaseAddress = new Uri("http://localhost:52611/");//URL de donde va a consumir el servicio rest(EndPoint)

                    var request = clienteHttp.GetAsync("api/Values/" + id).Result;// tipo de peticion de la URL
                    if (request.IsSuccessStatusCode)
                    {
                        string resultString = request.Content.ReadAsStringAsync().Result;//regresa un Json en un tipo string (Serealizar)

                        //********instalar del nuget newtonsoft.Json
                        mascota = JsonConvert.DeserializeObject<Mascotas>(resultString);
                    }
                    else
                    {
                        throw new Exception("Error al ejecutar el api mascotas");
                    }

                }
               
                return View(mascota);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return View(mascota);
            }
        }
        [HttpPost]
        public ActionResult Edit(Mascotas m)
        {

            try
            {
                using (HttpClient api = new HttpClient())
                {
                    // api.BaseAddress = new Uri("http://localhost:52611/"); 
                    //instalar la libreria del nuget system.Net.Formatting.Extension
                    //Para poder utilizar las entidades como parametros
                    //  var respuesta = api.PostAsync("api/Personas", p,new  JsonMediaTypeFormatter()).Result;

                    var postTask = api.PutAsJsonAsync<Mascotas>("http://localhost:52611/api/Values", m);

                    if (postTask.Result.IsSuccessStatusCode)
                    {
                        TempData["m"] = "Se editó correctamente";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        throw new Exception("error de comunicación Post Api");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public ActionResult Delete(int id)
        {
            Mascotas mascota = new Mascotas();
            try
            {
                using (HttpClient clienteHttp = new HttpClient())//instancia del servicio rest
                {
                    clienteHttp.BaseAddress = new Uri("http://localhost:52611/");//URL de donde va a consumir el servicio rest(EndPoint)

                    var request = clienteHttp.DeleteAsync("api/Values/" + id).Result;// tipo de peticion de la URL
                    if (request.IsSuccessStatusCode)
                    {
                        string resultString = request.Content.ReadAsStringAsync().Result;//regresa un Json en un tipo string (Serealizar)

                        //********instalar del nuget newtonsoft.Json
                        mascota = JsonConvert.DeserializeObject<Mascotas>(resultString);
                    }
                    else
                    {
                        throw new Exception("Error al ejecutar el api mascotas");
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Buscar(string valor)
        {
            List<Mascotas> mascotas = new List<Mascotas>();
            try
            {
                using (HttpClient clienteHttp = new HttpClient())
                {
                    clienteHttp.BaseAddress = new Uri("http://localhost:52611/");

                    var request = clienteHttp.GetAsync($"api/Values?valor={valor}").Result;

                


                    if (request.IsSuccessStatusCode)
                    {
                        string resultString = request.Content.ReadAsStringAsync().Result;
                        mascotas = JsonConvert.DeserializeObject<List<Mascotas>>(resultString);
                    }
                    else
                    {
                        throw new Exception("Error al ejecutar el API de búsqueda de mascotas");
                    }
                }
                return View("Index", mascotas); // Devuelve la vista de Index con los resultados de la búsqueda.
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return View("Index", mascotas); // También puedes mostrar un mensaje de error en la vista Index.
            }
        }

    }
}
