using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiMascotas.Models;

namespace WebApiMascotas.Controllers
{
    public class ValuesController : ApiController
    {
        Generacion22Entities db = new Generacion22Entities();

        // GET api/values
        [HttpGet]
        public List<Mascotas> Obtener()
        {
            List<Mascotas> lista = db.Mascotas.ToList();
            db.Dispose();
            return lista;
        }

        // GET api/values/5
        public Mascotas Get(int id)
        {
            Mascotas mascota = db.Mascotas.Find(id);
            db.Dispose();
            return mascota;
        }

        // POST api/values
        [HttpPost]
        public void Agregar(Mascotas mascota)
        {
            db.Mascotas.Add(mascota);
            db.SaveChanges();
            db.Dispose();
        }

        // PUT api/values/5
        public void Put(Mascotas mascota)
        {
            db.Mascotas.AddOrUpdate(mascota);

            //Mascotas mdb = db.Mascotas.Find(mascota.idMascota);
            //mdb.Nombre = mascota.nombreMascota;

            db.SaveChanges();
            db.Dispose();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Mascotas mascota = db.Mascotas.Find(id);
            db.Mascotas.Remove(mascota);
            db.SaveChanges();
            db.Dispose();
        }

        // GET api/values/valor
        [HttpGet]
        public List<Mascotas> Buscar(string valor)
        {
            List<Mascotas> lista = db.Mascotas.Where(x => x.nombre.Contains(valor)).ToList();
            db.Dispose();
            return lista;
        }
    }
}
